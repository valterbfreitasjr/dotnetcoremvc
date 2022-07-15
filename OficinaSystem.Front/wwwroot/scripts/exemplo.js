document.addEventListener("DOMContentLoaded", function () {
    abrirLoader();
    carregarDadosTabelaConvenio();
});
async function AlterarConvenio(codigo) {
    const response = await fetch('convenio/obterporid/' + codigo);
    const data = await response.json();
    document.getElementById('txtId').value = data.NCodConvenio;
    document.getElementById('txtDescricaoAlt').value = data.CDesc;
    jQuery('#modalAlterarRegistro').modal({
        backdrop: 'static',
        keyboard: false
    });

}
function AtualizaTable() {

    document.getElementById('destroy').innerHTML = "";
    document.getElementById('destroy').innerHTML = `
                    <table class="table table-striped" id="ConveniosCadastrados">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Descrição</th>
                                <th scope="col">Opções</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>    `;

    carregarDadosTabelaConvenio();
}



function RemoverConvenio(id) {

    jQuery('#modalConfirmarExclusaoConvenio').modal({
        backdrop: 'static',
        keyboard: false
    }).one('click', '#delete_convenio', function (e) {


        fetch('../../cadastro/convenio/remover', {
            method: 'post',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(id)
        }).then(res => res.json())
            .then(res => {

                if (res == 1) {

                    jQuery('#modalConfirmarExclusaoConvenio').modal('hide');
                    AtualizaTable();
                    toastr['success'](MSG_EXCLUIDO, TITULO_TOASTR_SUCESSO);

                }
                else if (res == -1)
                {
                    jQuery('#modalConfirmarExclusaoConvenio').modal('hide');
                    AtualizaTable();
                    toastr['error']("Não foi possível excluir, pois há dados vinculados a esse convênio!", TITULO_TOASTR_ERRO);
                }
                else {
                    jQuery('#modalConfirmarExclusaoConvenio').modal('hide');
                    toastr['success'](MSG_ERRO_EXCLUIR, TITULO_TOASTR_ERRO);
                }
            });

    });

}



document.getElementById("btnNovoRegistro").addEventListener("click", function () {
    ModalNovoRegistro();
});

function ModalNovoRegistro() {

    jQuery('#modalNovoRegistro').modal({
        backdrop: 'static',
        keyboard: false
    });
}


async function carregarDadosTabelaConvenio() {
    const response = await fetch('convenio/obtertodos');
    const data = await response.json();
    let tr = '';
    data.map(dado => {
        tr += `<tr>
               <td>${dado.nCodConvenio}</td>
               <td>${dado.cDesc}</td> 
               <td>
                    <button class="btn btn-icon btn-primary" id="btnAlterarConvenio${dado.nCodConvenio}" onclick="AlterarConvenio(${dado.nCodConvenio})"><i class="fa fa-edit"></i></button>
                    <button class="btn btn-icon btn-danger" id="btnRemoverConvenio${dado.nCodConvenio}" onclick="RemoverConvenio(${dado.nCodConvenio})"><i class="fa fa-trash"></i></button>
               </td>
               </tr>`;

    });
    jQuery('#ConveniosCadastrados > tbody').html(tr);
    let nomeTabela = 'ConveniosCadastrados';
    dataTable(nomeTabela);
    fecharLoader();
}


function AtualizarConvenio() {
    let descricao = document.getElementById('txtDescricaoAlt').value;
    let id = document.getElementById('txtId').value;

    const dto = {
        NCodConvenio: id,
        CDesc: descricao
    };

    document.getElementById('alterar_convenio').disabled = true;
    if (descricao.trim().length > 2) {

        fetch('../../cadastro/convenio/alterar', {
            method: 'post',
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dto)
        }).then(res => res.json())
            .then(res => {
                if (res == -1) {
                    toastr['error'](MSG_REGISTRO_DUPLICADO, TITULO_TOASTR_ERRO);
                }
                else if (res >= 1) {
                    toastr['success'](MSG_ATUALIZADO, TITULO_TOASTR_SUCESSO);
                    AtualizaTable();
                    jQuery('#modalAlterarRegistro').modal('hide');

                    $("#modalAlterarRegistro *").val("");
                } else {
                    toastr['error'](MSG_ERRO_ATUALIZAR, TITULO_TOASTR_ERRO);
                }
            });

    }
    else {
        toastr['error']('Campo descrição deve ter no mínimo 3 caracteres!', TITULO_TOASTR_ATENCAO);
    }

    document.getElementById('alterar_convenio').disabled = false;
}


function NovoConvenio() {
    let descricao = document.getElementById('txtDescricao').value;
    document.getElementById('salvar_convenio').disabled = true;
    if (descricao.trim().length > 2) {

        fetch('../../cadastro/convenio/adicionar', {
            method: 'post',
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(descricao)
        }).then(res => res.json())
            .then(res => {
                if (res == -1) {
                    toastr['error'](MSG_REGISTRO_DUPLICADO, TITULO_TOASTR_ERRO);
                }
                else if (res >= 1) {
                    toastr['success'](MSG_SUCESSO, TITULO_TOASTR_SUCESSO);
                    AtualizaTable();
                    jQuery('#modalNovoRegistro').modal('hide');
                    // document.getElementById('txtDescricao').value = '';
                    $("#modalNovoRegistro *").val("");
                } else {
                    toastr['error'](MSG_ERRO_INSERIR, TITULO_TOASTR_ERRO);
                }
            });

    }
    else {
        toastr['error']('Campo descrição deve ter no mínimo 3 caracteres!', TITULO_TOASTR_ATENCAO);
    }

    document.getElementById('salvar_convenio').disabled = false;
}

document.getElementById("salvar_convenio").addEventListener("click", function () {
    NovoConvenio();
});

document.getElementById("alterar_convenio").addEventListener("click", function () {
    AtualizarConvenio();
});

