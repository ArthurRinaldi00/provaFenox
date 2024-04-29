let _modelCores = {
    idCores: 0,
    descricao: "",
    status: 0
}

let _modelCombustivel = {
    IdCombustivel: 0,
    DescricaoComb: "",
    StatusCombustivel: 0
}

let _modelVeiculo = {
    IdVeiculo: 0,
    Placa: "",
    Renavam: 0,
    NChassi: 0,
    NMotor:"",
    Marca:"",
    Modelo:"",
    Combustivel:0,
    Cor:0,
    Ano:"",
    StatusVeiculo:0
}

function ShowCores(){
    fetch("/Cores/GetAllCoresList")
    .then(Response =>{
        return Response.ok ? Response.json():Promise.reject(Response);
    })   
    .then(ResponseJson => {
        if(ResponseJson.length>0){
            $("#tabelaCores").html("");
            ResponseJson.forEach((Cor)=>{
                $("#tabelaCores").append(
                    $("<tr>").append(
                        $("<td>").text(Cor.descricao),
                        $("<td>").text(Cor.status),
                        $("<td>").append(
                            $("<button>").addClass("btn btn-primary btn-sm button-edit-Cor").text("Edit").data("dataCor",Cor),
                            $("<button>").addClass("btn btn-danger btn-sm button-delete-Cor ms-2").text("Delete").data("dataCor",Cor),
                        )
                    )
                )
                
            })
        }
    })
}


function OpenModal() {
    $("#txtDescricaoCor").val(_modelCores.descricao);
    $("#txtStatusCor").val(_modelCores.status);

    //show modal
    $("#ModalCores").modal("show");

}

$(document).on("click", ".button-new-cor", function () {
    _modelCores.idCores = 0;
    _modelCores.descricao = "";
    _modelCores.status = 0;

    OpenModal();
})

$(document).on("click", ".button-edit-cor", function () {
    _modelCores.idCores = 0;
    _modelCores.descricao = "";
    _modelCores.status = 0;

    OpenModal();
})

$(document).on("click", ".button-save-changes-cor", function () {

    const model = {
        IdCores: _modelCores.idCores,
        Descricao: $("#txtDescricaoCor").val(),
        Status: $("#txtStatusCor").val()
    }
    console.log(model)

    if (_modelCores.idCores === 0) {

        fetch("/Cores/InsertCores", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(model),
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.value) {
                    $("#ModalCores").modal("hide");
                    alert("Cor ja registrada");
                    ShowCores();
                }
                else
                    alert("Erro")

            })

    } else {
console.log("bateu update");
        fetch("/Home/Cores/EditCores", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(model)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.value) {
                    $("#ModalCores").modal("hide");
                    alert("Cor Foi Alterada");
                    ShowCores();
                }
                else
                    alert("Erro")
            })
    }
})

$(document).on("click", ".button-delete-cor", function () {
console.log("bateu delete")
    const _Cor = $(this).data("dataCor");

    const result = confirm(`A cor que deseja excluir é: "${_Cor.descricao}" ?`);

    if (result) {
        fetch(`/Cores/DeleteCores?idCores=${_Cor.IdCores}`, {
            method: "DELETE"
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.value) {
                    alert("Cor foi excluida!");
                    ShowCores();
                } else
                    alert("Erro")
            })
    }
})


function ShowCombustivel(){
    fetch("/Combustivel/GetAllCombustivelList")
    .then(Response =>{
        return Response.ok ? Response.json():Promise.reject(Response);
    })   
    .then(ResponseJson => {
        if(ResponseJson.length>0){
            $("#tabelaCombustivel").html("");
            ResponseJson.forEach((Combustivel)=>{
                $("#tabelaCores").append(
                    $("<tr>").append(
                        $("<td>").text(Combustivel.descricao),
                        $("<td>").text(Combustivel.status),
                        $("<td>").append(
                            $("<button>").addClass("btn btn-primary btn-sm button-edit-Cor").text("Edit").data("dataComb",Combustivel),
                            $("<button>").addClass("btn btn-danger btn-sm button-delete-Cor ms-2").text("Delete").data("dataComb",Combustivel),
                        )
                    )
                )
                
            })
        }
    })
}


function OpenModalCombustivel() {
    $("#txtDescricaoCombustivel").val(_modelCombustivel.descricao);
    $("#txtStatusCombustivel").val(_modelCombustivel.status);

    //show modal
    $("#ModalCombustivel").modal("show");
}
