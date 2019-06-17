

#include "rota_drone.h"
#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <stdbool.h>

#define MAX 0x3f3f3f;
#define PREDACAO 0 //ligar ou desligar a predação

// step 1
Angles *init_pop() {
    Angles *pop = (Angles *)malloc(POP * sizeof(Angles));

    if (pop == NULL) {
        printf("ERROR! pop is null\n");
        exit(0);
    }

    int i, j;
    int pi20000 = PI * 20000;

    srand(time(NULL));
    for (i = 0; i < POP; i++) {
        //cada "indivíduo" (rota) possui um número de ARMS e ARMS - 1 dobras
        pop[i].val = (double *)malloc(ARMS * sizeof(double));

        for (int j = 0; j < ARMS; j++) {
            pop[i].val[j] = rand() % pi20000 * 1.0 / 10000;
        }
    }

    return pop;
}

OBSTACULOS* init_obstaculos(){
    OBSTACULOS* obstaculos = (OBSTACULOS*) malloc(NUM_OBSTACULOS * sizeof(OBSTACULOS));
    int i, j;
    int coordenadas[NUM_OBSTACULOS * 2 + 1] = {40.0, 10.0, 50.0, 50.0, 60.0, 30.0, 35.0, 20.0, 50.0, 50.0};
    for(i = 0, j = 0; i < (NUM_OBSTACULOS * 2); i+=2, j++){
        obstaculos[j].cord[0] = coordenadas[i];
        obstaculos[j].cord[1] = coordenadas[i+1];
    }
    return obstaculos;
}

// step 2
// evalute, in this case the less the better
double fitness(double origin[2], double goal[2], double *angles, OBSTACULOS *obstaculos) {

    double x, y, prevx = origin[0], prevy = origin[1], distance_obs = 0.0, closest_distance = MAX;
    double penalidade = 0.0, distance = 0.0;
    int pi20000 = PI * 20000;
    int i;
    for (i = 0; i < ARMS; i++){
        x = prevx + cos(angles[i])*ARM_SIZE;
        y = prevy + sin(angles[i])*ARM_SIZE;
        prevx = x;
        prevy = y;

        //calculando as distancias até os obstaculos
        for(int j = 0; j < NUM_OBSTACULOS; j++){
            distance = sqrt((x - obstaculos[j].cord[0])*(x - obstaculos[j].cord[0]) + (y - obstaculos[j].cord[1])*(y - obstaculos[j].cord[1]));
            if(distance < closest_distance){
                closest_distance = distance;
            }
            distance_obs += distance;
        }

    }
    //se a distancia até algum dos obstaculos for muito pequena adiciona-se uma penalidade alta 
    if(closest_distance < 5.0){
        penalidade = 3000.0;
        if(PREDACAO){
            //na predação, cria-se um novo indivíduo e retorna-se o fitness desse novo indivíduo
                for (int j = 0; j < ARMS; j++){
                angles[j] = rand()%pi20000*1.0/10000;   
            }
            return fitness(origin, goal, angles, obstaculos);
        }
    }
    //retorna a soma da distancia até o objetivo, a soma das distancias até o obstáculo e a penalidade de intersecção 
    //com algum obstáculo
    return sqrt((x - goal[0])*(x - goal[0]) + (y - goal[1])*(y - goal[1])) + (5000.0/ distance_obs) + penalidade;
}

// step 3
// return the index of the best
int selection(Angles *pop, double origin[2], double goal[2], OBSTACULOS *obstaculos) {
    int best = 0;
    for (int i = 1; i < POP; ++i) {
        if (fitness(origin, goal, pop[i].val, obstaculos) < fitness(origin, goal, pop[best].val, obstaculos)){
            best = i;
        }
    }
    return best;
}

// step 4, 5, 6
// crossover, mutation and new population
void crossover(Angles *pop, int best, double mutation_rate) {
    int i, j;

    for (i = 0; i < POP; i++) {

        if (i == best)
            continue;

        for (j = 0; j < ARMS; j++) {
            // crossover
            pop[i].val[j] += pop[best].val[j];
            pop[i].val[j] /= 2;

            // mutation
            if (rand() % 2)
                pop[i].val[j] *= (1 + mutation_rate);
            else
                pop[i].val[j] *= (1 - mutation_rate);
        }
    }
}

// show results
void show_result(Angles *pop, double origin[2], double goal[2], OBSTACULOS *obstaculos) {
    int i, j;

    for (i = 0; i < POP; ++i) {
        printf("Individual: %d: ", i);
        for (j = 0; j < ARMS; j++) {
         //   printf("%.4lf  ", pop[i].val[j]); //imprime os angulos
        }
        printf("Fitness: %.2lf\n", fitness(origin, goal, pop[i].val, obstaculos));
    }
    printf("Best index: %d\n\n", selection(pop, origin, goal, obstaculos));
}

// free memory
void destroy(Angles *pop) {
    int i;

    for (i = 0; i < POP; i++) {
        free(pop[i].val);
    }
    free(pop);
}

