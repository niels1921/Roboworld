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