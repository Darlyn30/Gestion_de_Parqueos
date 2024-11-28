const URL_API = "https://localhost:7058/api/Cuenta";//ESTA URL PUEDE CAMBIAR SI ES HTTP O HTPPS

const inputs = {
    email: document.getElementById("email"),
    pass: document.getElementById("password"),
}

const btnLog = document.querySelector("button");


function log(){

    fetch(URL_API)
    .then(res => res.json())
    .then(data => {
        

        for(let i = 0; i < data.length; i++){
            
            let id = inputs.email.value;
            let pass = inputs.pass.value;

            if(id == data[i].correo && pass == data[i].clave){
                swal({
                    title: "Inicio de sesion exitoso!",
                    text: `Bienvenido ${data[i].nombre}!`,
                    icon: "success",
                })
                .then(res => {
                    window.location = "../Dashboard/Dashboard.html";
                });
            }

            
        }
    })

}

btnLog.addEventListener("click", () => {
    event.preventDefault();
    log();
})