﻿using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class ProvaAlunoResultado : EntidadeBase
    {
        public ProvaAlunoResultado() { }

        public ProvaAlunoResultado(long provaId, long dreId, long ueId, long turmaId, string ano, Modalidade modalidade, int anoLetivo, DateTime inicio, DateTime fim, long alunoId, long alunoRa, string alunoNome, string alunoNomeSocial, int situacao, bool alunoDownload, DateTime? alunoInicio, DateTime? alunoFim, int? alunoTempo, int? alunoQuestaoRespondida, string usuarioIdReabertura, DateTime? dataHoraReabertura, SituacaoProvaAluno? situacaoProvaAluno)
        {
            ProvaId = provaId;
            DreId = dreId;
            UeId = ueId;
            TurmaId = turmaId;
            Ano = ano;
            Modalidade = modalidade;
            AnoLetivo = anoLetivo;
            Inicio = inicio;
            Fim = fim;
            AlunoId = alunoId;
            AlunoRa = alunoRa;
            AlunoNome = alunoNome;
            AlunoNomeSocial = alunoNomeSocial;
            AlunoSituacao = situacao;
            AlunoDownload = alunoDownload;
            AlunoInicio = alunoInicio;
            AlunoFim = alunoFim;
            AlunoTempo = alunoTempo;
            AlunoQuestaoRespondida = alunoQuestaoRespondida;
            UsuarioIdReabertura = usuarioIdReabertura;
            DataHoraReabertura = dataHoraReabertura;
            SituacaoProvaAluno = situacaoProvaAluno;

            Id = Guid.NewGuid().ToString();
        }

        public long ProvaId { get; set; }
        public long DreId { get; set; }
        public long UeId { get; set; }
        public long TurmaId { get; set; }
        public string Ano { get; set; }
        public Modalidade Modalidade { get; set; }
        public int AnoLetivo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public long AlunoId { get; set; }
        public long AlunoRa { get; set; }
        public string AlunoNome { get; set; }
        public string AlunoNomeSocial { get; set; }
        public int AlunoSituacao { get; set; }
        public bool AlunoDownload { get; set; }
        public DateTime? AlunoInicio { get; set; }
        public DateTime? AlunoFim { get; set; }
        public int? AlunoTempo { get; set; }
        public int? AlunoQuestaoRespondida { get; set; }
        public SituacaoProvaAluno? SituacaoProvaAluno { get; set; }
        public string UsuarioIdReabertura { get; set; }
        public DateTime? DataHoraReabertura { get; set; }

        /// <summary>
        /// Método criado para "driblar" o problema de exclusão do registro no Elasticsearch
        /// </summary>        
        public void InutilizarRegistro()
        {
            ProvaId = 0;
            DreId = 0;
            UeId = 0;
            TurmaId = 0;
            Inicio = DateTime.MinValue;
            Fim = DateTime.MinValue;
            AlunoId = 0;
            AlunoRa = 0;
            AlunoInicio = null;
            AlunoFim = null;
            SituacaoProvaAluno = null;
            DataHoraReabertura = null;
        }
    }
}
