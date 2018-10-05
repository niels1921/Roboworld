function ObjectLoader(path, obj, x, y, z, group, material) {
    new THREE.MTLLoader()
        .setPath(path)
        .load(material, function (materials) {
            materials.preload();
            new THREE.OBJLoader()
                .setMaterials(materials)
                .setPath(path)
                .load(obj, function (object) {
                    if (x !== null && y !== null && z !== null) {
                        group.position.set(x, y, z);
                    }
                    group.add(object);
                });
        });
}