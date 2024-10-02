drop table if exists games;
create table if not exists games (
	id serial not null primary key,
	nome varchar(100) not null,
	genero varchar(50) not null,
	classificacao varchar(20) not null,
	idiomas varchar(200) not null,
	preco real not null,
	multiplayer bool not null,
	config_minima varchar(500) not null,
	config_recomendada varchar(500) not null,
	resumo varchar(500) not null
);

select * from games;