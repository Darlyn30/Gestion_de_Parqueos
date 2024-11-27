
const h1 = document.querySelector(".W");

const sideBarLinks = document.querySelectorAll('.sidebar a');

const divs = {
    home: document.querySelector(".home"),
    disponibilidad: document.querySelector(".disponibilidad"),
    tarifas: document.querySelector(".tarifas"),
    vehiculosParqueo: document.querySelector(".vehiculosParqueo"),
    general: document.querySelector(".general"),

}

const imgs = {
    car: document.querySelector(".carro"),
    moto: document.querySelector(".motor"),
    trailer: document.querySelector(".camion"),
}

const btnResultCalc = document.querySelector(".btnResultCalc");

document.addEventListener("DOMContentLoaded", () => {
    home();
})



// Función para ejecutar una acción segun el enlace clickeado

function handleSectionClick(section){
    switch (section) {
        case 'Inicio':
            console.log('Acción para la sección Inicio');
            home();
          break;
        case 'Disponibilidad':
            console.log('Acción para la sección Disponibilidad');
            disponibilidad();
            break;
        case 'Calcular Tarifas':
            console.log('Acción para Calcular Tarifas');
            tarifas();
            break;
        case 'Vehiculos En El Parqueo':
            console.log('Acción para Vehículos en el Parqueo');
            vehiculosParqueo();
            break;
        case 'Registro General':
            console.log('Acción para Registro General');
            VistaGeneral();
            break;
    }
}


function home(){
    //en el Home
    const URL_API = "https://localhost:7058/api/Cuenta";

    divs.disponibilidad.style.display = "none";
    divs.tarifas.style.display = "none";
    divs.vehiculosParqueo.style.display = "none";
    divs.general.style.display = "none";
    divs.home.style.display = "block";


    fetch(URL_API) //esto es para el tema del nombre y eso
    .then(res => res.json())
    .then(data => {
        for(let i = 0; i < data.length; i++){

            h1.innerHTML = `Bienvenido, ${data[i].nombre}`
        }
    })
}





function disponibilidad(){
    const URL_API_VIEW_AVAILABLE_PARKING = "https://localhost:7058/api/VistaDisponibilidad";

    divs.tarifas.style.display = "none";
    divs.vehiculosParqueo.style.display = "none";
    divs.general.style.display = "none";
    divs.home.style.display = "none";
    divs.disponibilidad.style.display = "block";

    const tableBody = document.getElementById('crudTable').querySelector('tbody');
    const row = document.createElement('tr');

    /**
     * esto hace que el cuerpo de la tabla se reinicie cada vez que se ejecute,
     * ya que es lo unico que debe mostrar, no debe agregar mas nada
     */
    tableBody.innerHTML = '';
    
    fetch(URL_API_VIEW_AVAILABLE_PARKING)
    .then(res => res.json())
    .then(data => {
        for(let i = 0; i < data.length; i++){
            const row = document.createElement("tr");
            row.innerHTML = `
            <td>${data[i].tipo}</td>
            <td>${data[i].totalDisponibles}</td>
            <td>${data[i].ocupados}</td>
            <td>$ ${data[i].precio} /h</td>
            `;
            tableBody.appendChild(row);
            
        }
    })
}


function tarifas(){
    divs.disponibilidad.style.display = "none";
    divs.vehiculosParqueo.style.display = "none";
    divs.general.style.display = "none";
    divs.home.style.display = "none";
    divs.tarifas.style.display = "block";


    btnResultCalc.addEventListener("click", async () => {


        const vehicleType = document.getElementById('vehicleType').value;
        const timeAmount = parseInt(document.getElementById('timeAmount').value);
        const timeUnit = document.getElementById('timeUnit').value;
    
        const URL_API_CALC_TARIFA = `https://localhost:7058/api/CalcularTarifa?vehiculoId=${vehicleType}&formato=${timeUnit}&cantidad=${timeAmount}`;
    
    
        
    
        fetch(URL_API_CALC_TARIFA, {
            method: 'GET',
        })
        .then(res => res.json())
        .then(data => {
            result(timeAmount, timeUnit, vehicleType, data);
        })
    
    })
}




function result(timeAmount, timeUnit, vehicleType, totalPrice){
    // Mostrar el resultado en el div
    const resultMessage = `El costo de ${timeAmount} ${timeUnit} de estacionamiento para un ${vehicleType} es: ${totalPrice}`;
    document.getElementById('resultMessage').innerText = resultMessage;
    
    // Hacer visible el div con el resultado
    document.getElementById('tariffResult').style.display = 'block';
}

function vehiculosParqueo(){

    divs.disponibilidad.style.display = "none";
    divs.tarifas.style.display = "none";
    divs.general.style.display = "none";
    divs.home.style.display = "none";
    divs.vehiculosParqueo.style.display = "block";

    const URL_API_VIEW_PARKING_NOW = "https://localhost:7058/api/IngresoAuto";

    fetch(URL_API_VIEW_PARKING_NOW)
    .then(response => response.json())
    .then(data => {
        const tbody = document.getElementById('tbodyVehiculos');
        const noDataDiv = document.getElementById('noData');

        // Limpiar la tabla antes de insertar nuevos datos
        tbody.innerHTML = '';

        if (data.length === 0) {
            // Si no hay vehículos, mostrar el mensaje
            noDataDiv.style.display = 'block';
        } else {
            // Si hay vehículos, ocultar el mensaje de no datos
            noDataDiv.style.display = 'none';

            // Recorrer los vehículos y agregar las filas a la tabla
            data.forEach(vehiculo => {
                const tr = document.createElement('tr');
                tr.innerHTML = `
                    <td>${vehiculo.codigo}</td>
                    <td>${vehiculo.tipoVehiculo}</td>
                    <td>${new Date(vehiculo.hora_entrada).toLocaleString()}</td>
                    <td>$</td>
                `;
                tbody.appendChild(tr);
                
            });
            
        }
        
    })
}

function VistaGeneral(){
    
    divs.disponibilidad.style.display = "none";
    divs.tarifas.style.display = "none";
    divs.vehiculosParqueo.style.display = "none";
    divs.home.style.display = "none";
    divs.general.style.display = "block";
}


//SIDEBAR ACTIVE METHOD


// Agrega un evento de clic a cada enlace
sideBarLinks.forEach(link => {
    link.addEventListener('click', function(event) {
        event.preventDefault();  // Evitar que el enlace realice su accion predeterminada (navegar)

        // Remover la clase 'active' de todos los enlaces
        sideBarLinks.forEach(l => l.classList.remove('active'));

        // Agregar la clase 'active' al enlace clickeado
        link.classList.add('active');

        // Llamar a la funcion handleSectionClick con el texto del enlace
        handleSectionClick(link.textContent);
    });
});