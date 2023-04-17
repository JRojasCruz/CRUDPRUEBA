// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let departamentos = [];
let provincias = [];
let distritos = [];

async function obtenerDepartamento() {
    const response = await fetch("/Departamentos")
    const data = await response.json()
    return data
}
async function obtenerProvincia() {
    const response = await fetch("/Provincias")
    const data = await response.json()
    return data
}
async function obtenerDistrito() {
    const response = await fetch("/Distritos")
    const data = await response.json()
    return data
}

function obtenerLocalizacion() {
    Promise.all([
        obtenerDepartamento(),
        obtenerProvincia(),
        obtenerDistrito()
    ]).then(res => {
        departamentos = res[0]
        provincias = res[1]
        distritos = res[2]
    });
}
function registrarTrabajador() {
    departamentos.forEach(d => {
        $("#cIdDepartamento").append(`<option value="${d.id}">${d.nombreDepartamento}</option>`)
    });
    provincias.forEach(d => {
        $("#cIdProvincia").append(`<option value="${d.Id}">${d.NombreProvincia}</option>`)
    });
    distritos.forEach(d => {
        $("#cIdDistrito").append(`<option value="${d.Id}">${d.NombreDistrito}</option>`)
    });
    $("#cformId").attr("action", `Trabajadores/Create`);
    //$("#cTipoDocumento").val(tipodocumento);
    //$("#cIdDepartamento").val(iddepartamento);
    //$("#cIdProvincia").val(idprovincia);
    //$("#cIdDistrito").val(iddistrito);
}
document.getElementById('registrarEmpleado').addEventListener('shown.bs.modal', () => {
    document.getElementById("cformId").reset()
    registrarTrabajador()
})
function editarTrabajador(id, tipodocumento, numdocumento, nombres, sexo, iddepartamento, idprovincia, iddistrito) {
    departamentos.forEach(d => {
        $("#eIdDepartamento").append(`<option value="${d.id}">${d.nombreDepartamento}</option>`)
    });
    provincias.forEach(d => {
        $("#eIdProvincia").append(`<option value="${d.Id}">${d.NombreProvincia}</option>`)
    });
    distritos.forEach(d => {
    $("#eIdDistrito").append(`<option value="${d.Id}">${d.NombreDistrito}</option>`)
    });
    $("#formId").attr("action",`Trabajadores/Edit/${id}`);
    $("#eId").val(id);
    $("#eTipoDocumento").val(tipodocumento);
    $("#eNumeroDocumento").val(numdocumento);
    $("#eSexo").val(sexo);
    $("#eNombres").val(nombres);
    $("#eIdDepartamento").val(iddepartamento);
    $("#eIdProvincia").val(idprovincia);
    $("#eIdDistrito").val(iddistrito);
}
function eliminarTrabajador(id) {
    $('#formEliminar').attr('action', `Trabajadores/Delete/${id}`)
}
document.getElementById('pintarFilas').addEventListener('change', function () {
    if (this.checked) {
        $('#tablaEmpleados').addClass('pintar')
    } else {
        $('#tablaEmpleados').removeClass('pintar')
    }
})
obtenerLocalizacion();
