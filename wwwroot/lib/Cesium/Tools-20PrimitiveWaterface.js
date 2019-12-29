var PrimitiveWaterFace = (
    function () {
        var degreesArrayHeights;
        var fragmentShader;
        var normalMapUrl;
        var geometry;
        var appearance;
        var viewer;
        function _(options) {//options是传进来的一组参数
            viewer = options.viewer;//一组参数中的viewer
            fragmentShader = FSWaterFace();//工具中定义好的
            normalMapUrl = options.normalMapUrl;//参数传进来的
            if (options.DegreesArrayHeights && options.DegreesArrayHeights.length >= 3) {
                degreesArrayHeights = options.DegreesArrayHeights;
                
            } else {
                degreesArrayHeights = [
                         118.04437, 60.10932, 100,
                         116.04537, 30.10932, 100,
                         116.04537, 30.11032, 100,
                         116.04437, 30.10932, 100];//假如不指定，默认就在这个位置
            }      
            if (options.extrudedHeight) {//传递的参数如果有拉伸高度的话
                geometry = CreateGeometry(degreesArrayHeights, options.extrudedHeight);//利用传进来的范围和拉伸的高度创建几何体
            } else {
                geometry = CreateGeometry(degreesArrayHeights);
            }
            
            appearance = CreateAppearence(fragmentShader, normalMapUrl);
            this.primitive = viewer.scene.primitives.add(new Cesium.Primitive({
                allowPicking: false,
                geometryInstances: new Cesium.GeometryInstance({
                    geometry: geometry
                }),
                appearance: appearance,
                asynchronous: false
            }));
        }
        //_degreesArrayHeights是一个组成多边形顶点数组[lon,lat,alt]
        function CreateGeometry(_degreesArrayHeights, _extrudedHeight) {
            return new Cesium.PolygonGeometry({
                polygonHierarchy: new Cesium.PolygonHierarchy(Cesium.Cartesian3.fromDegreesArrayHeights(_degreesArrayHeights)),
                extrudedHeight: _extrudedHeight?_extrudedHeight:0,
                perPositionHeight: true
            });
        }


        function CreateAppearence(fs, url) {
            return new Cesium.EllipsoidSurfaceAppearance({
                material: new Cesium.Material({
                    fabric: {
                        type: 'Water',
                        uniforms: {
                            normalMap: url,
                            frequency: 5000.0,
                            animationSpeed: 1,
                            amplitude: 5000.0
                        }
                    }
                }),
                fragmentShaderSource: fs
            });
        }
         
        function FSWaterFace() {
            return 'varying vec3 v_positionMC;\n\
varying vec3 v_positionEC;\n\
varying vec2 v_st;\n\
\n\
void main()\n\
{\n\
    czm_materialInput materialInput;\n\
    vec3 normalEC = normalize(czm_normal3D * czm_geodeticSurfaceNormal(v_positionMC, vec3(0.0), vec3(10.0)));\n\
#ifdef FACE_FORWARD\n\
    normalEC = faceforward(normalEC, vec3(0.0, 0.0, 10.0), -normalEC);\n\
#endif\n\
    materialInput.s = v_st.s;\n\
    materialInput.st = v_st;\n\
    materialInput.str = vec3(v_st, 0.0);\n\
    materialInput.normalEC = normalEC;\n\
    materialInput.tangentToEyeMatrix = czm_eastNorthUpToEyeCoordinates(v_positionMC, materialInput.normalEC);\n\
    vec3 positionToEyeEC = -v_positionEC;\n\
    materialInput.positionToEyeEC = positionToEyeEC;\n\
    czm_material material = czm_getMaterial(materialInput);\n\
#ifdef FLAT\n\
    gl_FragColor = vec4(material.diffuse + material.emission, material.alpha);\n\
#else\n\
    gl_FragColor = czm_phong(normalize(positionToEyeEC), material);\n\
    gl_FragColor.a = 0.8;\n\
#endif\n\
}\n\
';
        }


        _.prototype.remove = function () {
            if (this.primitive != null) {
                viewer.scene.primitives.remove(this.primitive);
                this.primitive = null;
            }
        }
        _.prototype.updateDegreesPosition = function (_degreesArrayHeights, _extrudedHeight) {
            if (this.primitive != null) {
                viewer.scene.primitives.remove(this.primitive);
                if (_degreesArrayHeights && _degreesArrayHeights.length < 3) { return; }
                geometry = CreateGeometry(_degreesArrayHeights, _extrudedHeight?_extrudedHeight:0);
               
                this.primitive = viewer.scene.primitives.add(new Cesium.Primitive({
                    allowPicking: false,
                    geometryInstances: new Cesium.GeometryInstance({
                        geometry: geometry
                    }),
                    appearance: appearance,
                    asynchronous: false
                }));
            } else { return; }
        }
        return _;
    })();