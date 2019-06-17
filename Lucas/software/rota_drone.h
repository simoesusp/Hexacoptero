

#ifndef ROTA_DRONE_H
#define ROTA_DRONE_H

#define POP 5   // number of individuals
#define ARMS 10 // number of arms
#define PI 3.14159265359
#define ARM_SIZE 10.0
#define NUM_OBSTACULOS 5

typedef struct angles {
    double *val;
} Angles;

typedef struct OBSTACULOS{
	double cord[2];
}OBSTACULOS;

Angles *init_pop();

OBSTACULOS *init_obstaculos();

double fitness(double origin[2], double goal[2], double *angles, OBSTACULOS *obstaculos);

int selection(Angles *pop, double origin[2], double goal[2], OBSTACULOS *obstaculos);

void crossover(Angles *pop, int best, double mutation_rate);

void show_result(Angles *pop, double origin[2], double goal[2], OBSTACULOS *obstaculos);

void destroy(Angles *pop);

#endif
