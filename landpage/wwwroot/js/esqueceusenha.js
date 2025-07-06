document.addEventListener('DOMContentLoaded', function () {
    console.log('carregou esqueceusenha.js');

    const formEmail = document.getElementById('form-es-senha');
    const formCodigo = document.getElementById('form-codigo-email');
    const formNovaSenha = document.getElementById('mudar-senha');
    const voltarBtns = document.querySelectorAll('a.btn-secondary');

    formEmail.addEventListener('submit', function (e) {
        e.preventDefault();
        formEmail.style.display = 'none';
        formCodigo.style.display = 'block';
    });

    formCodigo.addEventListener('submit', function (e) {
        e.preventDefault();
        formCodigo.style.display = 'none';
        formNovaSenha.style.display = 'block';
    });

    formNovaSenha.addEventListener('submit', function (e) {
        e.preventDefault();

        const senha = document.getElementById('nova-senha').value;
        const confirma = document.getElementById('confirma-senha').value;

        if (senha !== confirma) {
            Swal.fire({
                icon: 'error',
                title: 'Erro',
                text: 'As senhas não coincidem!',
                confirmButtonColor: '#dc3545'
            });
            return;
        }

        Swal.fire({
            icon: 'success',
            title: 'Senha redefinida com sucesso',
            text: 'Você será redirecionado para o login',
            showConfirmButton: true,
            confirmButtonText: 'Ir agora',
            confirmButtonColor: '#28a745',
            allowOutsideClick: false,
            allowEscapeKey: false,
            timer: 3000,
            timerProgressBar: true
        }).then((result) => {
            if (result.isConfirmed || result.dismiss === Swal.DismissReason.timer) {
                window.location.href = '/Account/LoginRegister';
            }
        });
    });

    voltarBtns.forEach(btn => {
        btn.addEventListener('click', function (e) {
            if (btn.getAttribute('href') === '/Account/LoginRegister') return;
            e.preventDefault();
            if (formCodigo.style.display === 'block') {
                formCodigo.style.display = 'none';
                formEmail.style.display = 'block';
            } else if (formNovaSenha.style.display === 'block') {
                formNovaSenha.style.display = 'none';
                formCodigo.style.display = 'block';
            }
        });
    });

    document.querySelectorAll('.code-input').forEach((input, idx, arr) => {
        input.addEventListener('input', function () {
            if (this.value.length === 1 && idx < arr.length - 1) {
                arr[idx + 1].focus();
            }
        });
        input.addEventListener('keydown', function (e) {
            if (e.key === 'Backspace' && !this.value && idx > 0) {
                arr[idx - 1].focus();
            }
        });
    });
});
