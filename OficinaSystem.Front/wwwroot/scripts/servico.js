document.addEventListener("DOMContentLoaded", function () {
    carregarDadosTabela();
});

document.getElementById("btnNovoRegistro").addEventListener("click", function () {
    ModalNovoRegistro();
});

document.getElementById("salvar_registro").addEventListener("click", function () {
    NovoRegistro();
});

document.getElementById("alterar_registro").addEventListener("click", function () {
    AtualizarRegistro();
});

function ModalNovoRegistro() {

    jQuery('#modalNovoRegistro').modal({
        backdrop: 'static',
        keyboard: false
    });
}

async function carregarDadosTabela() {
    const response = await fetch('servico/obtertodos');
    const data = await response.json();
    let tr = '';
    data.map(dado => {
        tr += `<tr>
               <td>${dado.id}</td>
               <td>${dado.descricao}</td> 
               <td>${dado.preco}</td> 
               <td>
                    <button class="btn btn-icon btn-primary" id="btnAlterar${dado.id}" onclick="Alterar(${dado.id})"><i class="fa fa-edit"></i></button>
                    <button class="btn btn-icon btn-danger" id="btnRemover${dado.id}" onclick="Remover(${dado.id})"><i class="fa fa-trash"></i></button>
               </td>
               </tr>`;

    });
    jQuery('#ServicoCadastrado > tbody').html(tr);
    let nomeTabela = 'ServicoCadastrado';
    dataTable(nomeTabela);
    fecharLoader();
}

function NovoRegistro() {

    let descricao = document.getElementById('txtDescricao').value;
    let preco = document.getElementById('txtPreco').value;

    const dto = {
        Descricao: descricao,
        Preco: preco,
    };

    document.getElementById('salvar_registro').disabled = true;

    fetch('servico/adicionar', {
        method: 'post',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(dto)
    }).then(res => res.json())
        .then(res => {
            if (res != null) {
                toastr['success']("Inserido Com sucesso", "Inserido");
                AtualizaTable();
                jQuery('#modalNovoRegistro').modal('hide');
                $("#modalNovoRegistro *").val("");
            } else {
                toastr['error']("Erro ao inserir", "Erro");
            }
        });

    document.getElementById('salvar_registro').disabled = false;
}

function AtualizaTable() {

    document.getElementById('destroy').innerHTML = "";
    document.getElementById('destroy').innerHTML = `
                    <table class="table table-striped" id="ServicoCadastrado">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Descrição</th>
                                <th scope="col">Preço</th>
                                <th scope="col">Opções</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>    `;

    carregarDadosTabela();
}

function AtualizarRegistro() {
    let id = document.getElementById('txtId').value;
    let descricao = document.getElementById('txtDescricaoAlt').value;
    let preco = document.getElementById('txtPrecoAlt').value;

    const dto = {
        Id: id,
        Descricao: descricao,
        Preco: preco,
    };

    document.getElementById('alterar_registro').disabled = true;

    fetch('produto/alterar', {
        method: 'post',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(dto)
    }).then(res => res.json())
        .then(res => {
            if (res >= 1) {
                toastr['success']("Registro Atualizado", "Sucesso");
                AtualizaTable();
                jQuery('#modalAlterarRegistro').modal('hide');

                $("#modalAlterarRegistro *").val("");
            } else {
                toastr['error']("Erro ao Atualizar", "Erro");
            }
        });


    document.getElementById('alterar_registro').disabled = false;
}

function AtualizarRegistro() {
    let id = document.getElementById('txtId').value;
    let descricao = document.getElementById('txtDescricaoAlt').value;
    let preco = document.getElementById('txtPrecoAlt').value;

    const dto = {
        Id: id,
        Descricao: descricao,
        Preco: preco,
    };

    document.getElementById('alterar_registro').disabled = true;

    fetch('servico/alterar', {
        method: 'post',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(dto)
    }).then(res => res.json())
        .then(res => {
            if (res >= 1) {
                toastr['success']("Registro Atualizado", "Sucesso");
                AtualizaTable();
                jQuery('#modalAlterarRegistro').modal('hide');

                $("#modalAlterarRegistro *").val("");
            } else {
                toastr['error']("Erro ao Atualizar", "Erro");
            }
        });


    document.getElementById('alterar_registro').disabled = false;
}

async function Alterar(codigo) {

    const response = await fetch('servico/obterporid/' + codigo);
    const data = await response.json();
    document.getElementById('txtId').value = data.id;
    document.getElementById('txtDescricaoAlt').value = data.descricao;
    document.getElementById('txtPrecoAlt').value = data.preco;

    jQuery('#modalAlterarRegistro').modal({
        backdrop: 'static',
        keyboard: false
    });

}

function Remover(id) {

    jQuery('#modalConfirmarExclusao').modal({
        backdrop: 'static',
        keyboard: false
    }).one('click', '#delete_registro', function (e) {


        fetch('servico/remover', {
            method: 'post',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(id)
        }).then(res => res.json())
            .then(res => {

                if (res == 1) {

                    jQuery('#modalConfirmarExclusao').modal('hide');
                    AtualizaTable();
                    toastr['success']("Registro Excluido", "Sucesso");

                }
                else {

                    jQuery('#modalConfirmarExclusao').modal('hide');
                    AtualizaTable();
                    toastr['error']("Não foi possível excluir, pois há dados vinculados a esse registro!", "Erro");
                }
            });

    });

}