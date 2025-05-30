-- Tabela de clientes
CREATE TABLE clientes (
    id SERIAL PRIMARY KEY,
    nome_completo TEXT NOT NULL,
    email TEXT NOT NULL UNIQUE,
    cpf VARCHAR(14) NOT NULL UNIQUE,
    data_nascimento DATE NOT NULL,
    senha_hash TEXT NOT NULL
);

-- Tabela de voos
CREATE TABLE voos (
    id SERIAL PRIMARY KEY,
    data_partida TIMESTAMP NOT NULL,
    data_chegada TIMESTAMP NOT NULL,
    numero_aviao INT NOT NULL 
);

-- Tabela de assentos
CREATE TABLE assentos (
    id SERIAL PRIMARY KEY,
    voo_id INT REFERENCES voos(id) ON DELETE CASCADE,
    tipo VARCHAR(20) NOT NULL CHECK (tipo IN ('ECONOMICA', 'PRIMEIRA')),
    numero_assento VARCHAR(5) NOT NULL,
    ocupado BOOLEAN DEFAULT FALSE
);

-- Tabela de compras
CREATE TABLE compras (
    id SERIAL PRIMARY KEY,
    cliente_id INT REFERENCES clientes(id),
    voo_id INT REFERENCES voos(id),
    data_compra TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    forma_pagamento VARCHAR(20) NOT NULL CHECK (forma_pagamento = 'PIX'),
    codigo_confirmacao UUID NOT NULL
);

-- Itens da compra (assentos comprados)
CREATE TABLE itens_compra (
    id SERIAL PRIMARY KEY,
    compra_id INT REFERENCES compras(id) ON DELETE CASCADE,
    assento_id INT REFERENCES assentos(id)
);

-- Índices
CREATE INDEX idx_voo_data ON voos(data_partida);
CREATE INDEX idx_assento_voo ON assentos(voo_id, ocupado);