function loadOBJModel(modelPath, modelName, textureName, onload) {
    new THREE.MTLLoader()
        .setPath(modelPath)
        .load(textureName, function (materials) {

            materials.preload();

            new THREE.OBJLoader()
                .setPath(modelPath)
                .setMaterials(materials)
                .load(modelName, function (object) {
                    onload(object);
                }, function () { }, function (e) { console.log("Error loading model"); console.log(e); });
        });
}

function addPointLight(object, color, x, y, z, intensity, distance) {
    var light = new THREE.PointLight(color, intensity, distance);
    light.position.set(x, y, z);
    object.add(light);
}

function addSpotLight(object, color, intensity, x, y, z, tx, ty, tz) {
    var spotlight = new THREE.SpotLight(color, intensity, 100, 0.5, 2, 2);
    spotlight.position.set(x, y, z);
    spotlight.castShadow = true;
    object.add(spotlight);
    object.add(spotlight.target);
    spotlight.target.position.set(tx, ty, tz);
}