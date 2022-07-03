# BPichincha-CasoPractico
Prueba tecnica Banco Pichincha

Solución BPichincha.DebitoCredito

Ejecución Docker

docker build -t debitocreditoapi . --no-cache && 
docker run -p 5000:80 -t debitocreditoapi --net=host

# Para verificar el sitio
http://localhost:5000/swagger/index.html