class Truck extends THREE.Group {
    constructor() {
        super();
        this.init();
    }

    init() {
        var selfref = this;
        loadOBJModel("/textures/truck/", "truck3.obj", "truck3.mtl", (mesh) => {
            mesh.rotation.y = Math.PI / 2;
            selfref.add(mesh);
        });
    }
}