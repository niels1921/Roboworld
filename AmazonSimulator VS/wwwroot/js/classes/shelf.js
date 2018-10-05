class Shelf extends THREE.Group {
    constructor(){
    super();
    this.init();
    }
    
    init() {
        var selfref = this;
        loadOBJModel("/textures/shelf/", "shelf4.obj", "shelf4.mtl", (mesh) => {
            selfref.add(mesh);
        });
    }
}