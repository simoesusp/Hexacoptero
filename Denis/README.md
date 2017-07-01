Resumo
===================

Nos últimos anos a tecnologia de Sistemas de Aeronaves Remotamente Pilotadas (SARPs) tem se mostrado progressivamente mais presente em iniciativas inovadoras para detecção, análise e monitoramento, tanto no campo militar como em áreas ligadas a economia, como agricultura e pecuária. Grande parte destas novas aeronaves, especificamente as de pequeno porte, são multirrotores: aeronaves de asas rotativas com dois ou mais motores. 

Usualmente, quando se tratando de um modelo de um aeroplano ou barco, o piloto tem precisão de controle sobre o motor, no qual um aumento no acelerador se traduz em um aumento proporcional de RPM. A diferença de multirrotores é o fato de ser muito difícil de um ser humano ser capaz de controlar a velocidade de rotação de três ou mais motores  simultaneamente com precisão suficiente para estabilizar a aeronave no ar. É nesta parte que se torna desejável a presença de um controlador de voo para estabilizar a aeronave. Este é constituído de uma placa de circuito impresso, contendo um processador e dispositivos eletrônicos como sensores e transmissores, e um software de complexidade variável. Sua função é controlar a rotação de cada motor em resposta aos comandos solicitados pelo usuário, em um esquema do tipo “fly-by-wire”.

Este trabalho tem como objetivo estudar e alterar o software do controlador ArduPilot de maneira a possibilitar que um sistema de comunicação sem fio em tempo real possa operar entre uma estação de solo e um multirrotor. O sistema será formado por um hexacóptero, um controlador de voo fixado na própria aeronave e um módulo de telemetria conectado em ambos os sistemas (multirrotor e base de solo). O controlador de voo será baseado em uma placa ArduPilot e se comunicará através do módulo de telemetria de frequência 433 MHz e com capacidade de transmitir dados até 250 kbps. Será desenvolvido também um software de controle que deve ser executado na estação de base e ser capaz de comandar remotamente a aeronave, transmitindo novas rotas de navegação e missões para o robô. Esse trabalho deve viabilizar uma interface de controle remoto que deverá possibilitar trabalhos futuros utilizando técnicas de inteligência artificial como inteligência de enxames e algoritmos evolutivos para controlar sistemas multirrobos.

# Sistema proposto pelo trabalho

![](http://i.imgur.com/LaAJZD2.jpg)

