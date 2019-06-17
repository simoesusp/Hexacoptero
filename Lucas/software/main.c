

#include "rota_drone.h"
#include <GL/glut.h>
#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

int NUM_GERACOES = 0;
double origin[2] = {40.0, 2.0}; // origin
double goal[2] = {60.0, 92.0};   // goal
OBSTACULOS* obstaculos;
double mutation_rate = 0.1;
Angles *pop;
int best;

void Draw(void) {

    // background colour
    glClearColor(0.0f, 0.0f, 0.0f, 1.0f);

    glClear(GL_COLOR_BUFFER_BIT);

    glPointSize(10.0f); // point size
    glLineWidth(5.0f);  // line width

    // draw origin(green) and goal(red)
    glBegin(GL_POINTS);
    glColor3f(0.0f, 1.0f, 0.0f);      // define green
    glVertex2f(origin[0], origin[1]); // draw origin

    glColor3f(1.0f, 0.0f, 0.0f);  // define red
    glVertex2f(goal[0], goal[1]); // draw goal


    glEnd();
  

    // draw all arms
    double x, y, prevx = origin[0], prevy = origin[1];
    int i, j;
    for (j = 0; j < POP; j++) {
        glBegin(GL_LINE_STRIP);      // draw lines in sequence
        glColor3f(0.5f, 0.5f, 0.5f); // define gray
        prevx = origin[0];
        prevy = origin[1];

        glVertex2f(prevx, prevy);

        for (i = 0; i < ARMS; i++) {
            x = prevx + cos(pop[j].val[i]) * ARM_SIZE;
            y = prevy + sin(pop[j].val[i]) * ARM_SIZE;
            prevx = x;
            prevy = y;

            glVertex2f(x, y);
        }

        glEnd();
    }
    for(i = 0; i < NUM_OBSTACULOS; i++){
        glBegin(GL_POINTS);
        glColor3f(1.0f, 1.0f, 0.0f); // define yellow
        glVertex2f(obstaculos[i].cord[0], obstaculos[i].cord[1]);                
        glEnd();
    }

    // draw best arms
    glBegin(GL_LINE_STRIP);
    glColor3f(1.0f, 1.0f, 1.0f);

    prevx = origin[0];
    prevy = origin[1];

    glVertex2f(prevx, prevy);
    for (i = 0; i < ARMS; i++) {
        x = prevx + cos(pop[best].val[i]) * ARM_SIZE;
        y = prevy + sin(pop[best].val[i]) * ARM_SIZE;
        prevx = x;
        prevy = y;

        glVertex2f(x, y);
    }

    glEnd();

    // send dates to draw
    glFlush();
}

void Mouse(int button, int state, int x, int y) {

    if (button == GLUT_LEFT_BUTTON) { // when LMB is actived
        goal[0] = x * 1.0 / 7;
        goal[1] = (700 - y) * 1.0 / 7;

        printf("Mouse x: %d, y: %d\n", x, y);

        glutPostRedisplay(); // redraw scene
    }

    if (button == GLUT_RIGHT_BUTTON) { // when LMB is actived
        origin[0] = x * 1.0 / 7;
        origin[1] = (700 - y) * 1.0 / 7;

        printf("Mouse x: %d, y: %d\n", x, y);

        glutPostRedisplay(); // redraw scene
    }
}

void Keyboard(unsigned char key, int x, int y) {

    // ENTER
    if (key == 13) {
        NUM_GERACOES++;
        crossover(pop, best, mutation_rate);
        best = selection(pop, origin, goal, obstaculos);
        show_result(pop, origin, goal, obstaculos);
        printf("-----Número de Gerações até aqui: %d------\n", NUM_GERACOES);

        glutPostRedisplay();
    }

    // reset
    if (key == 'r') {
        destroy(pop);
        printf("\n-----------------------------------------------\n");
        printf("\nOld population destroyed and a new initialized!\n\n");
	NUM_GERACOES = 0;
        pop = init_pop();
        best = selection(pop, origin, goal, obstaculos);
        show_result(pop, origin, goal, obstaculos);
        mutation_rate = 0.1;

        glutPostRedisplay();
    }

    // increase mutation rate
    if (key == 'p') {
        mutation_rate *= 2.0;
	printf("\n-----------------------------------------------\n");
        printf("\nMutation rate: %.2lf \n\n", mutation_rate );
    }


    // decrease mutation rate
    if (key == 'o') {
        mutation_rate /= 2.0;
	printf("\n-----------------------------------------------\n");
        printf("\nMutation rate: %.2lf \n\n", mutation_rate );
    }

    // ESC
    if (key == 27) {
        exit(0);
    }
}

// just explain the commands
void instruction() {
    printf("\nr: restart\n");
    printf("ENTER: new generation\n");
    printf("p: increase mutation rate\n");
    printf("o: decrease mutation rate\n");
    printf("LMB: change goal\n");
    printf("RMB: change origin\n");
    printf("ESC: exit\n\n");
    printf("-------------------------\n\n");
}

int main(int argc, char *argv[]) {

    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
    
    glutInitWindowSize(700, 700);
    glutCreateWindow("Rotas do Drone");

    glutDisplayFunc(Draw);
    glutMouseFunc(Mouse);
    glutKeyboardFunc(Keyboard);

    gluOrtho2D(0, 100, 0, 100);

    instruction();
    pop = init_pop();
    obstaculos = init_obstaculos();
    best = selection(pop, origin, goal, obstaculos);
    // show_result(pop, origin, goal);

    glutMainLoop();

    destroy(pop);

    return 0;
}
