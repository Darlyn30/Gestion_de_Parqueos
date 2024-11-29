const URL_API = "https://localhost:7058/api/Cuenta";//ESTA URL PUEDE CAMBIAR SI ES HTTP O HTPPS
const URL_API_SESION = "https://localhost:7058/api/Sesion";

const inputs = {
    email: document.getElementById("email"),
    pass: document.getElementById("password"),
}

const btnLog = document.querySelector("button");


function log(){

    let mail = inputs.email.value;
    let pass = inputs.pass.value;

    fetch(URL_API_SESION)
    .then(res => res.json())
    .then(data => {
        if(data == ""){
            console.log("no hay nada");
            
            fetch(URL_API)
            .then(res => res.json())
            .then(datos => {
                datos.forEach(account => {
                    if(mail == account.correo && pass == account.clave){
                        swal({
                            title: "Inicio de sesion exitoso!",
                            text: `Bienvenido ${account.nombre}!`,
                            icon: "success",
                        })
                        .then(res => {
                            const postSesionURL_API = `https://localhost:7058/api/Sesion?email=${mail}`;
                            fetch(postSesionURL_API, {
                                method: 'POST',
                                headers : {
                                    'Content-Type' : 'application/json'
                                }
                            })
                            .then(res => {
                                if(res.status == 200){

                                    window.location = "../Dashboard/Dashboard.html";
                                }
                            })
                        })
                    } else {
                        swal({
                            title: "Error de inicio de sesion",
                            text: `No se pudo encontrar su correo o contrase?a`,
                            icon: "warning",
                        })
                        .then(res => {
                            window.location = "./Login.html";
                        })
                    }
                })
            })
        }
    })

}



function sesionIniciada(){
    fetch(URL_API_SESION)
    .then(res => res.json())
    .then(data => {
        if(data != ""){
            window.location = "../DashBoard/Dashboard.html";
        }
    })
}



btnLog.addEventListener("click", () => {
    event.preventDefault();
    log();
})


sesionIniciada(); // siempre evalua en primera instancia, si hay alguna sesion iniciada


