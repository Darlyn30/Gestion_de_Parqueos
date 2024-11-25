const URL_API = "https://localhost:7058/api/Cuenta";

const h1 = document.querySelector(".W");

//en el Home
fetch(URL_API) //esto es para el tema del nombre y eso
.then(res => res.json())
.then(data => {
    for(let i = 0; i < data.length; i++){

        h1.innerHTML = `Bienvenido, ${data[i].nombre}`
    }
})

const URL_API_VIEW_AVAILABLE_PARKING = "https://localhost:7058/api/VistaDisponibilidad";

const tableBody = document.getElementById('crudTable').querySelector('tbody');
const row = document.createElement('tr');
let idCounter = 1;

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

//SIDEBAR ACTIVE METHOD
const sidebarLinks = document.querySelectorAll('.sidebar a');

// Agrega un evento de clic a cada enlace
sidebarLinks.forEach(link => {
  link.addEventListener('click', () => {
    // Elimina la clase 'active' de todos los enlaces
    sidebarLinks.forEach(l => l.classList.remove('active'));

    // Agrega la clase 'active' al enlace clicado
    link.classList.add('active');
  });
});