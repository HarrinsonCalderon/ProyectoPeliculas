using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Utilidades
{
    public class autoMapperProfiles:Profile
    {
        public autoMapperProfiles(GeometryFactory geometryFactory) {
            //el reversemap es de genereodto a genero
            //genero a generoDTO
            CreateMap<Genero, GenereoDTO>().ReverseMap();
            //GeneroCreacionDTO a genero
            CreateMap<GeneroCreacionDTO,Genero >();

            CreateMap<Actor, ActorDTO>().ReverseMap();
       
            CreateMap<ActorCreacionDTO, Actor>().ForMember(x=>x.Foto,
                options=>options.Ignore());
            CreateMap<CineCreacionDTO, Cine>().ForMember(x => x.ubicacion, x => x.MapFrom(dto =>
                  geometryFactory.CreatePoint(new Coordinate(dto.Longitud, dto.Latitud))));
            CreateMap<Cine, CineDTO>().ForMember(x => x.latitud, dto => dto.MapFrom(campo => campo.ubicacion.Y))
                .ForMember(x => x.latitud, dto => dto.MapFrom(campo => campo.ubicacion.X));
            CreateMap<PeliculaCreacionDTO, Pelicula>().
                ForMember(x => x.Poster, opciones => opciones.Ignore())
                .ForMember(x => x.PeliculasGeneros, opciones => opciones.MapFrom(MapearPeliculasGeneros))
                .ForMember(x => x.peliculasCines, opciones => opciones.MapFrom(MapearPeliculasCines))
                .ForMember(x => x.PeliculasActores, opciones => opciones.MapFrom(MapearPeliculasActores));
        }
        private List<PeliculasGeneros> MapearPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula) {
            var resultado = new List<PeliculasGeneros>();
            if (peliculaCreacionDTO.GenerosIds==null) {
                return resultado;
            }
            foreach (var id in peliculaCreacionDTO.GenerosIds)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id });
            }
            return resultado;
        }
        private List<PeliculasCines> MapearPeliculasCines(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasCines>();
            if (peliculaCreacionDTO.CinesIds == null)
            {
                return resultado;
            }
            foreach (var id in peliculaCreacionDTO.CinesIds)
            {
                resultado.Add(new PeliculasCines() { CineId = id });
            }
            return resultado;
        }
        private List<PeliculasActores> MapearPeliculasActores(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();
            if (peliculaCreacionDTO.GenerosIds == null)
            {
                return resultado;
            }
            foreach (var actor in peliculaCreacionDTO.Actores)
            {
                resultado.Add(new PeliculasActores() { ActorId = actor.Id, Personaje=actor.Personaje});
            }
            return resultado;
        }
    }
}
