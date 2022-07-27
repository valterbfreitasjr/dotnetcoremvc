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

async function carregarDadosTabela() {
    const response = await fetch('produto/obtertodos');
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
    jQuery('#ProdutoCadastrado > tbody').html(tr);
    let nomeTabela = 'ProdutoCadastrado';
    dataTable(nomeTabela);
    fecharLoader();
}

function ModalNovoRegistro() {

    jQuery('#modalNovoRegistro').modal({
        backdrop: 'static',
        keyboard: false
    });
}

function NovoRegistro() {

    let descricao = document.getElementById('txtDescricao').value;
    let preco = document.getElementById('txtPreco').value;

    const dto = {
        Descricao: descricao,
        Preco: preco,
    };

    document.getElementById('salvar_registro').disabled = true;

    fetch('produto/adicionar', {
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
                    <table class="table table-striped" id="ProdutoCadastrado">
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
    let nome = document.getElementById('txtNomeAlt').value;
    let cpf = document.getElementById('txtCpfAlt').value;
    let endereco = document.getElementById('txtEnderecoAlt').value;

    const dto = {
        Id: id,
        Nome: nome,
        Cpf: cpf,
        Endereco: endereco
    };

    document.getElementById('alterar_registro').disabled = true;

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


    document.getElementById('alterar_registro').disabled = false;
}

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


