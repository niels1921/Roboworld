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

        //cabin light
        //addPointLight(selfref, 0xffffff, 2.3, 0.6, 0.4, 1, 10);
        //headlight
        addPointLight(selfref, 0xffff00, 3.2, 0.45, 0.58, 0.5, 1);
        addSpotLight(selfref, 0xffff00, 0.2, 3.2, 0.45, 0.58, 5.2, 0.3, 0.58);
        addPointLight(selfref, 0xffff00, 3.2, 0.45, -0.58, 0.5, 1);
        addSpotLight(selfref, 0xffff00, 0.2, 3.2, 0.45, -0.58, 5.2, 0.3, 0.58);
        //backlight
        addPointLight(selfref, 0xff0000, -3.2, 0.45, 0.58, 0.5, 1);
        addPointLight(selfref, 0xff0000, -3.2, 0.45, -0.58, 0.5, 1);
        //var box = new THREE.Mesh(new THREE.BoxGeometry(0.5, 0.5, 0.5), new THREE.MeshBasicMaterial({ color: 0xffffff }));
        //box.position.set(2.3, 0.6, 0.4);
        //selfref.add(box);
    }
}