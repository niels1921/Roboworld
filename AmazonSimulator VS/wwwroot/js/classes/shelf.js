class Shelf extends THREE.group {
    constructor(){
    super();
    this.init();
    }
}

init() {
    if (command.parameters.type === "shelf") {
        var groupShelf = new THREE.Group();
        ObjectLoader("/textures/shelf/", "shelf4.obj", null, null, null, groupShelf, "shelf4.mtl")
        scene.add(groupShelf);
        worldObjects[command.parameters.guid] = groupShelf;
    }
}