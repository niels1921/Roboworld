if (command.parameters.type === "robot") {
    var geometryRobot = new THREE.BoxGeometry(0.9, 0.3, 0.9);
    var MaterialsRobot = [
        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot/robot_side.png"), side: THREE.DoubleSide }), //LEFT
        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot/robot_side.png"), side: THREE.DoubleSide }), //RIGHT
        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot/robot_top.png"), side: THREE.DoubleSide }), //TOP
        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot/robot_bottom.png"), side: THREE.DoubleSide }), //BOTTOM
        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot/robot_front.png"), side: THREE.DoubleSide }), //FRONT
        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot/robot_front.png"), side: THREE.DoubleSide }), //BACK
    ];
    var materialRobot = new THREE.MeshFaceMaterial(MaterialsRobot);
    var modelRobot = new THREE.Mesh(geometryRobot, materialRobot);
    modelRobot.position.y = 0.15;

    var groupRobot = new THREE.Group();
    groupRobot.add(modelRobot);

    scene.add(groupRobot);
    worldObjects[command.parameters.guid] = groupRobot;
}