all: rota_drone.o main.c
	@gcc -o main rota_drone.o main.c -lglut -lGLU -lGL -lm -Wall
rota_drone.o: rota_drone.c rota_drone.c
	@gcc -c rota_drone.c -o rota_drone.o
run:
	./main
clean:
	rm rota_drone.o main
