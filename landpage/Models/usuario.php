<?php
class UsuarioModel {
    private $conexao;

    public function __construct() {
        $this->conexao = new mysqli("localhost", "seu_usuario", "sua_senha", "nome_do_banco");
        if ($this->conexao->connect_error) {
            die("Erro de conexão: " . $this->conexao->connect_error);
        }
    }

    public function cadastrarConsumidor($email, $senha) {
        $senhaHash = password_hash($senha, PASSWORD_DEFAULT);
        $sql = "INSERT INTO consumidores (email, senha) VALUES (?, ?)";
        $stmt = $this->conexao->prepare($sql);
        $stmt->bind_param("ss", $email, $senhaHash);
        return $stmt->execute();
    }

    public function cadastrarVendedor($email, $senha) {
        $senhaHash = password_hash($senha, PASSWORD_DEFAULT);
        $sql = "INSERT INTO vendedores (email, senha) VALUES (?, ?)";
        $stmt = $this->conexao->prepare($sql);
        $stmt->bind_param("ss", $email, $senhaHash);
        return $stmt->execute();
    }
}
?>