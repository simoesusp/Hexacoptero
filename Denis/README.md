Resumo
===================

Nos �ltimos anos a tecnologia de Sistemas de Aeronaves Remotamente Pilotadas (SARPs) tem se mostrado progressivamente mais presente em iniciativas inovadoras para detec��o, an�lise e monitoramento, tanto no campo militar como em �reas ligadas a economia, como agricultura e pecu�ria. Grande parte destas novas aeronaves, especificamente as de pequeno porte, s�o multirrotores: aeronaves de asas rotativas com dois ou mais motores. 

Usualmente, quando se tratando de um modelo de um aeroplano ou barco, o piloto tem precis�o de controle sobre o motor, no qual um aumento no acelerador se traduz em um aumento proporcional de RPM. A diferen�a de multirrotores � o fato de ser muito dif�cil de um ser humano ser capaz de controlar a velocidade de rota��o de tr�s ou mais motores  simultaneamente com precis�o suficiente para estabilizar a aeronave no ar. � nesta parte que se torna desej�vel a presen�a de um controlador de voo para estabilizar a aeronave. Este � constitu�do de uma placa de circuito impresso, contendo um processador e dispositivos eletr�nicos como sensores e transmissores, e um software de complexidade vari�vel. Sua fun��o � controlar a rota��o de cada motor em resposta aos comandos solicitados pelo usu�rio, em um esquema do tipo �fly-by-wire�.

Este trabalho tem como objetivo estudar e alterar o software do controlador ArduPilot de maneira a possibilitar que um sistema de comunica��o sem fio em tempo real possa operar entre uma esta��o de solo e um multirrotor. O sistema ser� formado por um hexac�ptero, um controlador de voo fixado na pr�pria aeronave e um m�dulo de telemetria conectado em ambos os sistemas (multirrotor e base de solo). O controlador de voo ser� baseado em uma placa ArduPilot e se comunicar� atrav�s do m�dulo de telemetria de frequ�ncia 433 MHz e com capacidade de transmitir dados at� 250 kbps. Ser� desenvolvido tamb�m um software de controle que deve ser executado na esta��o de base e ser capaz de comandar remotamente a aeronave, transmitindo novas rotas de navega��o e miss�es para o rob�. Esse trabalho deve viabilizar uma interface de controle remoto que dever� possibilitar trabalhos futuros utilizando t�cnicas de intelig�ncia artificial como intelig�ncia de enxames e algoritmos evolutivos para controlar sistemas multirrobos.

# Sistema proposto pelo trabalho

![](http://i.imgur.com/LaAJZD2.jpg)

