document.addEventListener('DOMContentLoaded', function() {
    console.log('carregou esqueceusenha.js');
    const formEmail = document.getElementById('form-es-senha');
    const formCodigo = document.getElementById('form-codigo-email');
    const formNovaSenha = document.getElementById('mudar-senha');

    const voltarBtns = document.querySelectorAll('a.btn-secondary');

    formEmail.addEventListener('submit', function(e) {
        e.preventDefault();
        formEmail.style.display = 'none';
        formCodigo.style.display = 'block';
    });

    formCodigo.addEventListener('submit', function(e) {
        e.preventDefault();
        formCodigo.style.display = 'none';
        formNovaSenha.style.display = 'block';
    });

    formNovaSenha.addEventListener('submit', function(e) {
        e.preventDefault();
        alert('Senha alterada com sucesso!');
        window.location.href = '/Account/LoginRegister';
    });

    voltarBtns.forEach(btn => {
        btn.addEventListener('click', function(e) {
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
    
    function toggleSenha(id, btn) {
        const input = document.getElementById(id);
        if (input.type === "password") {
            input.type = "text";
            btn.innerText = "🙈";
        } else {
            input.type = "password";
            btn.innerText = "👁️";
        }
    }      

    document.querySelectorAll('.code-input').forEach((input, idx, arr) => {
        input.addEventListener('input', function() {
            if (this.value.length === 1 && idx < arr.length - 1) {
                arr[idx + 1].focus();
            }
        });
        input.addEventListener('keydown', function(e) {
            if (e.key === 'Backspace' && !this.value && idx > 0) {
                arr[idx - 1].focus();
            }
        });
    });
});