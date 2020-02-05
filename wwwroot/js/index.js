require.config({
    waitSeconds: 60, // 加载超时时间，单位为秒
    paths: {
        //Cesium: '../Build/Cesium/Cesium',
        Cesium: 'Cesium',    
        bootstrapTree: 'bootstrap-treeview',
     
    },
    shim: { // 配置非AMD规范模块
        Cesium: {
            exports: 'Cesium',
        },
        bootstrapTree: {
            exports: 'bootstrapTree'
        },
    }
})

function init(Cesium) {
    Cesium.Ion.defaultAccessToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIzZDMwNTQ3ZC0xYjE5LTQ5MTUtYmM4ZC0yOTgwYTg4ZDA0N2EiLCJpZCI6MTQwNzcsInNjb3BlcyI6WyJhc3IiLCJnYyJdLCJpYXQiOjE1NjQ3MDg4OTJ9.YUsZDqZPckX3GwlCCuqfoOVZokwMKdySqrkVgKUv5dA';
    var viewer;
    viewer = new Cesium.Viewer('cesiumContainer', {
        animation: false, //是否显示动画控件(左下方那个)
        //baseLayerPicker: false, //是否显示图层选择控件
        geocoder: false, //是否显示地名查找控件
        timeline: false, //是否显示时间线控件
        sceneModePicker: false, //是否显示投影方式控件
        navigationHelpButton: false, //是否显示帮助信息控件
        infoBox: true, //是否显示点击要素之后显示的信息
        baseLayerPicker: false,
        homeButton:false,
        selectionIndicator: false,//鼠标点击聚焦框
        contextOptions: {
            webgl: {
                alpha: true
            }
        },
    });
    //viewer.scene.globe.imageryLayers.removeAll();
    viewer.scene.globe.baseColor = new Cesium.Color(0, 0, 0, 0.01);
    viewer.scene.undergroundMode = true;
    viewer.scene.globe.depthTestAgainstTerrain = true;//地形遮挡
    viewer.scene.logarithmicDepthBuffer = false;
  
    //var imageryProvider = new Cesium.UrlTemplateImageryProvider({
    //    url: "",
    //    tilingScheme: new Cesium.WebMercatorTilingScheme(),
    //    fileExtension: 'png',
    //    minimumLevel: 0,
    //    maximumLevel: 17
    //});
    //viewer.imageryLayers.addImageryProvider(imageryProvider);

    viewer.camera.flyTo({
        destination: new Cesium.Cartesian3.fromDegrees(118.35503, 34.3664, 16447.93325518917),//如东经纬度
        duration: 5
    });
    // }
 
    //去除版权信息
    viewer._cesiumWidget._creditContainer.style.display = "none";

    //Cesium.Camera.DEFAULT_VIEW_RECTANGLE =Cesium.Rectangle.fromDegrees(80, 22, 130, 50);//修改homeButton默认位置为中国

    //监控相机高度判断是否显示影像
    var handler = new Cesium.ScreenSpaceEventHandler(viewer.scene.canvas);
    handler.setInputAction(function (wheelment) {

        var height = viewer.camera.positionCartographic.height;
        if (height < 4000) {
            for (var i = 0; i < viewer.imageryLayers._layers.length; i++) {
                var layer = viewer.imageryLayers._layers[i];
                //if (layer._imageryProvider.url == "") {
                //    layer.show = true;
                //}
                //if (layer._imageryProvider.url == "") {
                //    layer.show = false;
                //}
            }
        };
        if (height >= 4000) {
            for (var i = 0; i < viewer.imageryLayers._layers.length; i++) {
                var layer = viewer.imageryLayers._layers[i];
                //if (layer._imageryProvider.url == ) {
                //    layer.show = false;
                //}
                //if (layer._imageryProvider.url == "") {
                //    layer.show = true;
                //}
            }
        }
      
    }, Cesium.ScreenSpaceEventType.WHEEL);

  
    viewer.screenSpaceEventHandler.setInputAction(function leftClick(movement) {
       
        if (tilesetArray.length > 0) {
            var heading = Cesium.Math.toDegrees(viewer.camera.heading).toFixed(2);//当前方向 由北向东旋转的角度
            for (var i = 0; i < tilesetArray.length; i++) {
                update3dtilesMaxtrix(0, 0, -heading, tilesetArray[i]);
            }
        }

    }, Cesium.ScreenSpaceEventType.MOUSE_MOVE);
    function update3dtilesMaxtrix(rx, ry, rz, tileset) {
        //旋转
        var mx = Cesium.Matrix3.fromRotationX(Cesium.Math.toRadians(rx));
        var my = Cesium.Matrix3.fromRotationY(Cesium.Math.toRadians(ry));
        var mz = Cesium.Matrix3.fromRotationZ(Cesium.Math.toRadians(rz));
        var rotationX = Cesium.Matrix4.fromRotationTranslation(mx);
        var rotationY = Cesium.Matrix4.fromRotationTranslation(my);
        var rotationZ = Cesium.Matrix4.fromRotationTranslation(mz);
        //平移
        //var position = Cesium.Cartesian3.fromDegrees(params.tx, params.ty, params.tz);
        //var m = Cesium.Transforms.eastNorthUpToFixedFrame(position);
        var m = Cesium.Transforms.eastNorthUpToFixedFrame(tileset.boundingSphere.center);
        //旋转、平移矩阵相乘
        Cesium.Matrix4.multiply(m, rotationX, m);
        Cesium.Matrix4.multiply(m, rotationY, m);
        Cesium.Matrix4.multiply(m, rotationZ, m);
        //赋值给tileset
        tileset._root.transform = m;
    }
    var tilesetArray = new Array();
   
    return viewer;
}



