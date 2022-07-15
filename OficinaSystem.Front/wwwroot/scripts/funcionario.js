document.addEventListener("DOMContentLoaded", function () {
    carregarDadosTabela();
});

document.getElementById("btnNovoRegistro").addEventListener("click", function () {
    ModalNovoRegistro();
});

async function Alterar(codigo) {

    const response = await fetch('funcionario/obterporid/' + codigo);
    const data = await response.json();
    document.getElementById('txtId').value = data.id;
    document.getElementById('txtNomeAlt').value = data.nome;
    document.getElementById('txtCpfAlt').value = data.cpf;
    document.getElementById('txtEnderecoAlt').value = data.endereco;

    jQuery('#modalAlterarRegistro').modal({
        backdrop: 'static',
        keyboard: false
    });

}

function AtualizaTable() {

    document.getElementById('destroy').innerHTML = "";
    document.getElementById('destroy').innerHTML = `
                    <table class="table table-striped" id="FuncionarioCadastrado">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Nome</th>
                                <th scope="col">Cpf</th>
                                <th scope="col">Endereço</th>
                                <th scope="col">Opções</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>    `;

    carregarDadosTabela();
}

function ModalNovoRegistro() {

    jQuery('#modalNovoRegistro').modal({
        backdrop: 'static',
        keyboard: false
    });
}

async function carregarDadosTabela() {
    const response = await fetch('funcionario/obtertodos');
    const data = await response.json();
    let tr = '';
    data.map(dado => {
        tr += `<tr>
               <td>${dado.id}</td>
               <td>${dado.nome}</td> 
               <td>${dado.cpf}</td> 
               <td>${dado.endereco}</td> 
               <td>
                    <button class="btn btn-icon btn-primary" id="btnAlterar${dado.id}" onclick="Alterar(${dado.id})"><i class="fa fa-edit"></i></button>
                    <button class="btn btn-icon btn-danger" id="btnRemover${dado.id}" onclick="Remover(${dado.id})"><i class="fa fa-trash"></i></button>
               </td>
               </tr>`;

    });
    jQuery('#FuncionarioCadastrado > tbody').html(tr);
    let nomeTabela = 'FuncionarioCadastrado';
    dataTable(nomeTabela);
    fecharLoader();
}


function NovoRegistro() {

    let nome = document.getElementById('txtNome').value;
    let cpf = document.getElementById('txtCpf').value;
    let endereco = document.getElementById('txtEndereco').value;

    const dto = {
        Nome: nome,
        Cpf: cpf,
        Endereco: endereco
    };

    document.getElementById('salvar_registro').disabled = true;

    fetch('funcionario/adicionar', {
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


function AtualizarRegistro() {
    let id = document.getElementById('txtId').value;
    let nome = document.getElementById('txtNomeAlt').value;
    let cpf = document.getElementById('txtCpfAlt').value;
    let endereco = document.getElementById('txtEnderecoAlt').value;

    const dto = {
        Id: id,
        Nome: nome,
        Cpf: cpf,
        Endereco: endereco
    };

    document.getElementById('alterar_convenio').disabled = true;

    fetch('funcionario/alterar', {
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


    document.getElementById('alterar_convenio').disabled = false;
}


function Remover(id) {

    jQuery('#modalConfirmarExclusao').modal({
        backdrop: 'static',
        keyboard: false
    }).one('click', '#delete_registro', function (e) {


        fetch('funcionario/remover', {
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
                else if (res == -1) {
                    jQuery('#modalConfirmarExclusao').modal('hide');
                    AtualizaTable();
                    toastr['error']("Não foi possível excluir, pois há dados vinculados a esse registro!", "Erro");
                }
            });

    });

}


document.getElementById("salvar_registro").addEventListener("click", function () {
    NovoRegistro();
});

document.getElementById("alterar_registro").addEventListener("click", function () {
    AtualizarRegistro();
});
