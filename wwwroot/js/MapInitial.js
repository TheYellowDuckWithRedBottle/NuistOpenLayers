var map;
var labelType;
var winHeight;
var winWidth;

var popup;
var popupElement = $("#popup");
var pupupContent = $("#popup-content");
var popupCloser = $("#popup-closer");

$(function () {
    popupCloser.bind("click", function () {
        popup.setPosition(undefined);
        popCloser.blur();
        return false;
    }
    );

    initMap();

    winWidth = $(window).width;
    winHeight = $(window).winHeight - 300;

    initMap(){
        map = new ol.Map({
            layers: [
                new ol.layer.Tile({
                    source: new ol.source.OSM()
                })
            ],
            target: 'map',
            view: new ol.View({
                projec: ol.proj.get('EPSG3857'),
                center: [1.321448384766289E7, 3790079.31448777],
                minZoom: 13,
                zoom: 15,
                maxZoom:18
            })
        }
        );

        map.on('pointermove', function (e) {
            var pixel = map.getEventPixel(e.originalEvent);
            var hit = map.hasFeatureAtPixel(pixel);
            map.getTargetElement().style.cursor = hit ? 'pointer' : '';

            var coordinate = e.coordinate;
            if (feature) {
                var type = feature.get('type');
            }
        });

        map.on('singleclick', function (e) {
            var coordinate = e.coordinate;
            var feature = map.forEachFeatureAtPixel(e.pixel, function (feature, layer) { return feature });
            if (feature) {
                pupupContent.innerHTML = '';
                var type = feature.get('type');
                if (type == "river" || type == "Rver") {
                    showDetailInfo(feature);
                } else {
                    return;
                }
            }

        });

        var popup = new ol.Overlay({
            element: popupElement,
            autoPan: true,
            positioning: 'bottom-center',
            stopEvent: false,
            autoPanAnimation: {
                duration: 250
            }
        });
        map.addOverlay(popup);

    }


})
