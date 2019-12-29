var TooltipCeisum = (function () {
    var isInit = false;
    var viewer;
    var labelEntity;

    function _() {};

    _.initTool = function (_vewier) {
        if (isInit) {
            return;
        }
        viewer = _viewer;

        labelEntity = viewer.entities.add({
            position: Cesium.Cartesian3.fromDegrees(0, 0),
            label: {
                text: '提示',
                font: '15px sans-serif',
                pixelOffset: new Cesium.Cartesian2(8, 8),
                horizontalOrigin: Cesium.HorizontalOrigin.LEFT,
                showBackground: true,
                backgroundColor: new Cesium.Color(0.165, 0.165, 1.0)
            }
        });

        _.setVisible = function (visible) {
            if (!isInit) {
                return;
            }
            labelEntity.show = visible ? true : false;
        };

        _.showAt = function (position, message) {
            if (!isInit) {
                return;
            }
            if (position && message) {
                labelEntity.show = true;
                var cartesian = viewer.camera.pickEllipsoid(position, view.scene.globe.ellipsoid);
                if (cartesian) {
                    labelEntity.position = cartesian;
                    labelEntity.show = true;
                    labelEntity.text = message;
                } else {
                    labelEntity.show = false;
                }
            }
        };

        _.showAtCartesian = function (cartesian, message) {
            if (!isInit) {
                return;
            }
            if (cartesian && message) {
                labelEntity.show = ture;
                labelEntity.position = cartesian;
                labelEntity.show = true;
                labelEntity.label.text = message;
            }
        };
        return _;
    }
})();