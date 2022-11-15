using Dalamud.Game.ClientState.JobGauge.Types;
using System;
using System.Linq;
using XIVAutoAttack.Actions.BaseAction;
using XIVAutoAttack.Combos.CustomCombo;
using XIVAutoAttack.Data;
using XIVAutoAttack.Helpers;

namespace XIVAutoAttack.Combos.Basic;
internal abstract class DNCCombo_Base<TCmd> : CustomCombo<TCmd> where TCmd : Enum
{
    protected static DNCGauge JobGauge => Service.JobGauges.Get<DNCGauge>();

    public sealed override ClassJobID[] JobIDs => new ClassJobID[] { ClassJobID.Dancer };

    /// <summary>
    /// ��к
    /// </summary>
    public static BaseAction Cascade { get; } = new(ActionID.Cascade)
    {
        BuffsProvide = new[] { StatusID.SilkenSymmetry }
    };

    /// <summary>
    /// ��Ȫ
    /// </summary>
    public static BaseAction Fountain { get; } = new(ActionID.Fountain)
    {
        BuffsProvide = new[] { StatusID.SilkenFlow }
    };

    /// <summary>
    /// ����к
    /// </summary>
    public static BaseAction ReverseCascade { get; } = new(ActionID.ReverseCascade)
    {
        BuffsNeed = new[] { StatusID.SilkenSymmetry, StatusID.SilkenSymmetry2 },
    };

    /// <summary>
    /// ׹��Ȫ
    /// </summary>
    public static BaseAction Fountainfall { get; } = new(ActionID.Fountainfall)
    {
        BuffsNeed = new[] { StatusID.SilkenFlow, StatusID.SilkenFlow2 }
    };

    /// <summary>
    /// ���衤��
    /// </summary>
    public static BaseAction FanDance { get; } = new(ActionID.FanDance)
    {
        OtherCheck = b => JobGauge.Feathers > 0,
        BuffsProvide = new[] { StatusID.ThreefoldFanDance },
    };

    /// <summary>
    /// �糵
    /// </summary>
    public static BaseAction Windmill { get; } = new(ActionID.Windmill)
    {
        BuffsProvide = Cascade.BuffsProvide,
    };

    /// <summary>
    /// ������
    /// </summary>
    public static BaseAction Bladeshower { get; } = new(ActionID.Windmill)
    {
        BuffsProvide = Fountain.BuffsProvide,
    };

    /// <summary>
    /// ���糵
    /// </summary>
    public static BaseAction RisingWindmill { get; } = new(ActionID.RisingWindmill)
    {
        BuffsNeed = ReverseCascade.BuffsNeed,
    };

    /// <summary>
    /// ��Ѫ��
    /// </summary>
    public static BaseAction Bloodshower { get; } = new(ActionID.Bloodshower)
    {
        BuffsNeed = Fountainfall.BuffsNeed,
    };

    /// <summary>
    /// ���衤��
    /// </summary>
    public static BaseAction FanDance2 { get; } = new(ActionID.FanDance2)
    {
        OtherCheck = b => JobGauge.Feathers > 0,
        BuffsProvide = new[] { StatusID.ThreefoldFanDance },
    };

    /// <summary>
    /// ���衤��
    /// </summary>
    public static BaseAction FanDance3 { get; } = new(ActionID.FanDance3)
    {
        BuffsNeed = FanDance2.BuffsProvide,
    };

    /// <summary>
    /// ���衤��
    /// </summary>
    public static BaseAction FanDance4 { get; } = new(ActionID.FanDance4)
    {
        BuffsNeed = new[] { StatusID.FourfoldFanDance },
    };

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction SaberDance { get; } = new(ActionID.SaberDance)
    {
        OtherCheck = b => JobGauge.Esprit >= 50,
    };

    /// <summary>
    /// ������
    /// </summary>
    public static BaseAction StarfallDance { get; } = new(ActionID.StarfallDance)
    {
        BuffsNeed = new[] { StatusID.FlourishingStarfall },
    };

    /// <summary>
    /// ǰ�岽
    /// </summary>
    public static BaseAction EnAvant { get; } = new(ActionID.EnAvant, true, shouldEndSpecial: true);

    /// <summary>
    /// Ǿޱ���Ų�
    /// </summary>
    public static BaseAction Emboite { get; } = new(ActionID.Emboite, true)
    {
        OtherCheck = b => (ActionID)JobGauge.NextStep == ActionID.Emboite,
    };

    /// <summary>
    /// С�񽻵���
    /// </summary>
    public static BaseAction Entrechat { get; } = new(ActionID.Entrechat, true)
    {
        OtherCheck = b => (ActionID)JobGauge.NextStep == ActionID.Entrechat,
    };

    /// <summary>
    /// ��ҶС����
    /// </summary>
    public static BaseAction Jete { get; } = new(ActionID.Jete, true)
    {
        OtherCheck = b => (ActionID)JobGauge.NextStep == ActionID.Jete,
    };

    /// <summary>
    /// ���ֺ��ת
    /// </summary>
    public static BaseAction Pirouette { get; } = new(ActionID.Pirouette, true)
    {
        OtherCheck = b => (ActionID)JobGauge.NextStep == ActionID.Pirouette,
    };

    /// <summary>
    /// ��׼�貽
    /// </summary>
    public static BaseAction StandardStep { get; } = new(ActionID.StandardStep)
    {
        BuffsProvide = new[]
         {
                StatusID.StandardStep,
                StatusID.TechnicalStep,
            },
    };

    /// <summary>
    /// �����貽
    /// </summary>
    public static BaseAction TechnicalStep { get; } = new(ActionID.TechnicalStep)
    {
        BuffsNeed = new[]
         {
                StatusID.StandardFinish,
            },
        BuffsProvide = StandardStep.BuffsProvide,
    };

    /// <summary>
    /// ����֮ɣ��
    /// </summary>
    public static BaseAction ShieldSamba { get; } = new(ActionID.ShieldSamba, true)
    {
        OtherCheck = b => !Player.HaveStatus(false, StatusID.Troubadour,
            StatusID.Tactician1,
            StatusID.Tactician2,
            StatusID.ShieldSamba),
    };

    /// <summary>
    /// ����֮������
    /// </summary>
    public static BaseAction CuringWaltz { get; } = new(ActionID.CuringWaltz, true);

    /// <summary>
    /// ��ʽ����
    /// </summary>
    public static BaseAction ClosedPosition { get; } = new(ActionID.ClosedPosition, true)
    {
        ChoiceTarget = Targets =>
        {
            Targets = Targets.Where(b => b.ObjectId != Player.ObjectId && b.CurrentHp != 0 &&
            //Remove Weak
            !b.HaveStatus(false, StatusID.Weakness, StatusID.BrinkofDeath)
            //Remove other partner.
            && (!b.HaveStatus(false, StatusID.ClosedPosition2) | b.HaveStatus(true, StatusID.ClosedPosition2)) 
            ).ToArray();

            var targets = TargetFilter.GetJobCategory(Targets, Role.��ս);
            if (targets.Length > 0) return targets[0];

            targets = TargetFilter.GetJobCategory(Targets, Role.Զ��);
            if (targets.Length > 0) return targets[0];

            targets = Targets;
            if (targets.Length > 0) return targets[0];

            return null;
        },
    };

    /// <summary>
    /// ����֮̽��
    /// </summary>
    public static BaseAction Devilment { get; } = new(ActionID.Devilment, true);

    /// <summary>
    /// �ٻ�����
    /// </summary>
    public static BaseAction Flourish { get; } = new(ActionID.Flourish, true)
    {
        BuffsNeed = new[] { StatusID.StandardFinish },
        BuffsProvide = new[]
        {
                 StatusID.ThreefoldFanDance,
                 StatusID.FourfoldFanDance,
            },
        OtherCheck = b => InCombat,
    };

    /// <summary>
    /// ���˱���
    /// </summary>
    public static BaseAction Improvisation { get; } = new(ActionID.Improvisation, true);

    /// <summary>
    /// ������
    /// </summary>
    public static BaseAction Tillana { get; } = new(ActionID.Tillana)
    {
        BuffsNeed = new[] { StatusID.FlourishingFinish },
    };

}