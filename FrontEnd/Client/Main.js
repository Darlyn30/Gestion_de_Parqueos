const h1 = document.querySelector("h1");
const btns = {
    parking: document.querySelector(".parking"),
    retire: document.querySelector(".retire"),
}

const btnFind = document.getElementById("btnFind");

const divs = {
    eleccion: document.querySelector(".eleccion"),
    parking: document.querySelector(".parquear"),
    retire:  document.querySelector(".retirar"),
    cancel: document.querySelector(".cancel"),

    //divs de los containers de las imagenes
    car: document.getElementById("1"),
    moto: document.getElementById("2"),
    truck: document.getElementById("3"),
}

const vehicleType = {
    car: document.querySelector(".car"),
    moto: document.querySelector(".moto"),
    truck: document.querySelector(".truck"),
}

const inputFilterByCode = document.getElementById("inputCode");

function parkingVehicle(){


    divs.eleccion.style.display = "none";
    divs.retire.style.display = "none";
    divs.cancel.style.display = "block";
    divs.parking.style.display = "block";


    //ESTA SOLUCION SERA TEMPORAL, ES UNA MUY MALA PRACTICA Y ESTOY VIOLANDO EL DRY



    vehicleType.car.addEventListener("click", () => {
        let id = 1;
        const URL_REGISTER_CAR = `https://localhost:7058/api/IngresoAuto?id=${id}`;

        fetch(URL_REGISTER_CAR, {
            method: 'GET',
        })
        .then(res => res.json())
        .then(data => {
            console.log("Vehiculo Agregado con exito");
            swal({
                title: "Registro Exitoso!",
                text: `Se ha registrado su vehiculo [${"Automovil"}] Exitosamente!`,
                icon: "success",
            })
            .then(res => {
                window.location = "./Cliente.html";
            });
        })
    })

    vehicleType.moto.addEventListener("click", () => {
        let id = 2;
        const URL_REGISTER_CAR = `https://localhost:7058/api/IngresoAuto?id=${id}`;
        fetch(URL_REGISTER_CAR, {
            method: 'GET',
        })
        .then(res => res.json())
        .then(data => {
            console.log("Vehiculo Agregado con exito");
            swal({
                title: "Registro Exitoso!",
                text: `Se ha registrado su vehiculo [${"Motocicleta"}] Exitosamente!`,
                icon: "success",
            })
            .then(res => {
                window.location = "./Cliente.html";
            });
        })
    })

    vehicleType.truck.addEventListener("click", () => {
        let id = 3;
        const URL_REGISTER_CAR = `https://localhost:7058/api/IngresoAuto?id=${id}`;
        fetch(URL_REGISTER_CAR, {
            method: 'GET'
        })
        .then(res => res.json())
        .then(data => {
            console.log(data);
            console.log("Vehiculo Agregado con exito");
            swal({
                title: "Registro Exitoso!",
                text: `Se ha registrado su vehiculo [${"Camion"}] Exitosamente!`,
                icon: "success",
            })
            .then(res => {
                window.location = "./Cliente.html";
            });
        })
    })

}



function retireVehicle(){

    divs.parking.style.display = "none";
    divs.eleccion.style.display = "none";
    divs.cancel.style.display = "block";
    divs.retire.style.display = "block";

    const URL_API_FILTER = "https://localhost:7058/api/IngresoAuto";

    
    
    fetch(URL_API_FILTER)
    .then(res => res.json())
    .then(data => {
        data.forEach(Codigo => {
            
            btnFind.addEventListener("click", () => {
                const vehicleType = document.getElementById('vehicleType').value;

                let inputCode = inputFilterByCode.value;
                let tipo; 
                switch(vehicleType){
                    case "1":
                        tipo = "Automovil";
                        break;
                    case "2":
                        tipo = "Motocicleta";
                        break;
                    case "3":
                        tipo = "Camion";
                        break;
                    }
                    console.log(vehicleType);
                    const URL_RETIRE_CAR = `https://localhost:7058/api/IngresoAuto?id=${vehicleType}&code=${Codigo.codigo}`

                if(inputCode == Codigo.codigo && tipo == Codigo.tipoVehiculo){

                    swal("Desea Retirar el vehiculo con el ID de parqueo: " + Codigo.codigo, {
                        dangerMode: true,
                        buttons: true,
                    })
                    .then(res => {
                        fetch(URL_RETIRE_CAR, {
                            method: 'DELETE',
                            headers: {
                                'Content-Type' : 'application/json'
                            }

                        })
                        .then(res => {
                            if(res.status == 200){
                                swal({
                                    title: "Peticion Exitosa!",
                                    text: `Vehiculo Retirado de forma exitosa!`,
                                    icon: "success",
                                })
                                .then(res => {
                                    window.location = "./Cliente.html";
                                })
                            }
                        })
                    });
                } else {
                    swal({
                        title: "Vehiculo no encontrado",
                        text: `No se pudo encontrar el vehiculo de ID ${inputCode} tipo: ${tipo}`,
                        icon: "error",
                    })
                }
            })

        });
    })

    

}

btns.retire.addEventListener("click", () => {
    retireVehicle();
})

btns.parking.addEventListener("click", () => {
    parkingVehicle();
})

h1.addEventListener("click", () => {
    divs.retire.style.display = "none";
    divs.cancel.style.display = "none";
    divs.parking.style.display = "none";
    divs.eleccion.style.display = "block";
})