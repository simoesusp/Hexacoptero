% 1. Inicio / Setup
clear all
close all
clc

global Kp Kv video printgeracao;

% Setup
video = true;
salvar = true;
plotar = true;

tamPop = 1; % Define o tamanho de populacao - OK
geracoes = 1; % Define o numero de geracoes - OK
maxVar = 5000; %Define valor maximo das variaveis de inicio - OK

mutacao = 10; % Define peridiocidade em geracoes para ocorrer a taxa de mutacao - Checar
taxMut = 5; %Define a ordem de divisao da mutacao

preda = 10; % Define a peridiocidade em geracoes da predacao

% Variaveis Auxiliares

i = 1;
melhor = 1;
printgeracao = 1;

% 2. Criar populacao Inicial
% Kp - Individuo1 
% Kv - Individuo2

for i=1:tamPop
    
   individuo1(i) = round( maxVar*rand() ) ; 
   individuo2(i) = round( maxVar*rand() ) ;
   
end

% 3. Loop 

for geracaoAtual = 1:geracoes


    for i=1:tamPop % 4.calculo do Fitness (avaliacao dos individuos)

       Kp = individuo1(i);
       Kv = individuo2(i);

       Kp = 93.796;
       Kv = 18.197;
       
       run runsim;

       settlingTime(i) = sim_info.SettlingTime;
       overshoot(i) = (sim_info.Max-z_des)*100;

       
       if overshoot(i) < 0;
           overshoot(i) = 1;
       else 
           overshoot(i) = overshoot(i) + 1;
       end

       
       fitness(i) =  settlingTime(i) + overshoot(i) ;

       if isnan( fitness(i) ) ;
           fitness(i) = 100; %Fitness máximo de 100 (pior valor)
       end
       
       
        delete(findall(0,'Type','figure'))
    
    end

    % 5. selecao do Melhor
    
    melhor = 1;
    
    for i=1:tamPop
        if fitness(i)<=fitness(melhor)
            melhor = i;
        end
    end
    
    % 5. Cross-Over

    %Swapping
    auxSwap1 = 1;
    auxSwap2 = 1;

    auxSwap1 = individuo1(melhor);
    auxSwap2 = individuo2(melhor); 
    
    individuo1(melhor) = individuo1(1);
    individuo2(melhor) = individuo2(1);
    
    individuo1(1) = auxSwap1;
    individuo2(1) = auxSwap2;
    
        %Obs: O fitness e suas componentes nao estao "swapping"
    
    %Cross-Over
    
    for i=2:tamPop
        
        individuo1(i)=( individuo1(i) + individuo1(1) )/2;
        individuo2(i)=( individuo2(i) + individuo2(1) )/2;
    end

% 6. predacao / Novos 2 Individuos (A cada "preda" geracoes)

    if rem(geracaoAtual,preda) == 0;
       individuo1(tamPop) = round( maxVar*rand() );
       individuo2(tamPop) = round( maxVar*rand() );
       individuo1(tamPop-1) = round( maxVar*rand() );
       individuo2(tamPop-1) = round( maxVar*rand() );
    end
    
% 7. mutacao (A cada "mutacao" geracoes sempre no segundo individuo)  
    
     if rem(geracaoAtual,mutacao) == 0;
       individuo1(2) = individuo1(2) + round( maxVar*rand()/taxMut );
       individuo2(2) = individuo2(2) + round( maxVar*rand()/taxMut );
     end

%8. Salva o historico
    
    aux_fit_historico = 1;
    
    fit_grupo_historico(geracaoAtual) = mean(fitness);
    fit_melhor_historico(geracaoAtual) = min(fitness);
    
    printgeracao = printgeracao+1;
    
    %Para gráfico de dispersão
    
    for i=1:tamPop
     dispersao(1,geracaoAtual*tamPop+i-tamPop) = individuo1(i);
     dispersao(2,geracaoAtual*tamPop+i-tamPop) = individuo2(i);
    end

   
    
end
% 9. Voltar para *



%% Salvar historico e Resultados

for i=1:length(dispersao)
    scat1(i) = dispersao(1,i);
    scat2(i) = dispersao(2,i);
end

if salvar
    csvwrite('scat1_200.csv',scat1)
    csvwrite('scat2_200.csv',scat2)
    csvwrite('dispersao_200.csv',dispersao)
    csvwrite('fit_grupo_historico_200.csv',fit_grupo_historico)
    csvwrite('fit_melhor_historico_200.csv',fit_melhor_historico)
end

%% Plot
    
if plotar
    
 geracao_plot = 1:length(fit_grupo_historico);

   
    
    figure(1)
    scatter(scat1, scat2, 'filled')
    title('Análise de Dispesão do Segundo Sistema')
    xlabel('Ganho do Controlador Proporcional  (Kp)')
    ylabel('Ganho do Controlador Derivativo (Kd)')

     
    figure(2)
    plot(geracao_plot ,fit_grupo_historico ,'r')
    hold on
    plot(geracao_plot,fit_melhor_historico)
    title('Fitness Médio da populacao vs do Melhor em cada geração')
    xlabel('Geração')
    ylabel('Fitness')


    figure(3)
    plot(geracao_plot,fit_grupo_historico,'r')
    hold on
    plot(geracao_plot,fit_melhor_historico)
    title('Fitness Médio da populacao vs Fitness do Melhor Indivíduo por geração')
    legend('Fitness Médio da Populaçao','Fitness do Melhor Indivíduo')
    xlabel('Geração')
    ylabel('Fitness')
    ylim([1.8 5])
    xlim([0 max(geracao_plot)]) 
    
end



%% Melhor Individuo
Kp = 93.796;
Kv = 18.197;

run runsim;

settlingTime(i) = sim_info.SettlingTime;
overshoot(i) = (sim_info.Max-z_des)*100;

%% plot do melhor individuo

figure(10)
plot(t,z)
title('Resposta do Degrau Unitário do Melhor Individuo')
xlabel('Tempo (s)')
ylabel('z (m)')
ylim([0 1.1])
xlim ([0 5])
grid on
