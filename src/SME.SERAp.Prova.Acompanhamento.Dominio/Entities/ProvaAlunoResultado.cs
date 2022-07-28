﻿using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class ProvaAlunoResultado : EntidadeBase
    {
        public ProvaAlunoResultado(int anoLetivo, long provaId, DateTime inicio, DateTime fim, long dreId, long ueId, string ano, Modalidade modalidade, long turmaId, long alunoId, long alunoRa, string alunoNome, string alunoNomeSocial, bool alunoDownload, DateTime? alunoInicio, DateTime? alunoFim, int? alunoTempoMedio, int? alunoQuestaoRespondida)
        {
            AnoLetivo = anoLetivo;
            ProvaId = provaId;
            Inicio = inicio;
            Fim = fim;
            DreId = dreId;
            UeId = ueId;
            Ano = ano;
            Modalidade = modalidade;
            TurmaId = turmaId;
            AlunoId = alunoId;
            AlunoRa = alunoRa;
            AlunoNome = alunoNome;
            AlunoNomeSocial = alunoNomeSocial;
            AlunoDownload = alunoDownload;
            AlunoInicio = alunoInicio;
            AlunoFim = alunoFim;
            AlunoTempoMedio = alunoTempoMedio;
            AlunoQuestaoRespondida = alunoQuestaoRespondida;

            Id = Guid.NewGuid().ToString();
        }

        public int AnoLetivo { get; set; }
        public long ProvaId { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public long DreId { get; set; }
        public long UeId { get; set; }
        public string Ano { get; set; }
        public Modalidade Modalidade { get; set; }
        public long TurmaId { get; set; }
        public long AlunoId { get; set; }
        public long AlunoRa { get; set; }
        public string AlunoNome { get; set; }
        public string AlunoNomeSocial { get; set; }
        public bool AlunoDownload { get; set; }
        public DateTime? AlunoInicio { get; set; }
        public DateTime? AlunoFim { get; set; }
        public int? AlunoTempoMedio { get; set; }
        public int? AlunoQuestaoRespondida { get; set; }
    }
}