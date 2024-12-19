# Projeto de Reconhecimento de Medidores de Gás e Eletricidade com IA

Este projeto é uma API que utiliza Inteligência Artificial (IA) para processar imagens de medidores de gás e eletricidade. O usuário envia a imagem codificada em base64, o tipo de medição (gás ou eletricidade), e o código do cliente. A API realiza a leitura do medidor, salva os dados no banco de dados e retorna as informações junto com a URL da imagem.

## Funcionalidades

- Recebe imagens de medidores de gás ou eletricidade em base64.
- Processa a imagem utilizando IA para detectar o consumo no medidor.
- Salva a leitura no banco de dados.
- Retorna os dados da leitura em formato JSON, incluindo a URL da foto.

## Tecnologias Utilizadas

- **ASP.NET Core** para construção da API.
- **Swagger** para documentação e testes da API.
- **IA (Reconhecimento de Imagens)** para processar as imagens e realizar a leitura dos medidores.
- **PostgreSQL** para armazenamento dos dados.
- **Base64** para o envio de imagens através de requisições HTTP.

## Endpoints

### `POST /api`

Este endpoint permite o envio de uma imagem em base64, o tipo do medidor e o código do cliente. A API processa a imagem, realiza a leitura e retorna os dados da medição.

#### Request

```json
{
  "image64": "<imagem_em_base64>",
  "measurementType": "GAS",  // Ou ELECTRICITY
  "costumerCode": "12345"
}
```
#### Response

- **200 OK** - Leitura do medidor processada com sucesso.

  Exemplo de resposta:

  ```json
  {
    "costumerCode": "12345",
    "measurementType": "GAS",  // Ou ELECTRICITY
    "imageUrl": "<url_da_imagem>",
    "ocrResult": "<result_ocr>",
    "processedAt": "<data_de_hoje>"
  }
  ```

- **400 Bad Request** - A solicitação foi malformada (por exemplo, imagem ou tipo do medidor não fornecidos corretamente).


## Como a IA Funciona

A IA no backend processa a imagem do medidor utilizando técnicas de visão computacional e reconhecimento óptico de caracteres (OCR). Ela identifica os números visíveis no medidor e retorna o valor do consumo, que é então salvo no banco de dados junto com a URL da imagem.

## Contribuições

Contribuições são bem-vindas! Se você quiser ajudar a melhorar este projeto, siga as etapas abaixo:

1. Faça um fork deste repositório.
2. Crie uma branch para sua feature (`git checkout -b feature-nova`).
3. Faça as alterações necessárias e commit (`git commit -am 'Adiciona nova feature'`).
4. Envie para o repositório remoto (`git push origin feature-nova`).
5. Abra um pull request.

## Licença

Este projeto está licenciado sob a MIT License - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
