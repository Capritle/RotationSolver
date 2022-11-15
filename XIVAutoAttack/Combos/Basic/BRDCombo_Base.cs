using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;
using System;
using System.Linq;
using XIVAutoAttack.Actions;
using XIVAutoAttack.Actions.BaseAction;
using XIVAutoAttack.Combos.CustomCombo;
using XIVAutoAttack.Data;
using XIVAutoAttack.Helpers;
using XIVAutoAttack.Updaters;

namespace XIVAutoAttack.Combos.Basic;

internal abstract class BRDCombo_Base<TCmd> : CustomCombo<TCmd> where TCmd : Enum
{
    protected static BRDGauge JobGauge => Service.JobGauges.Get<BRDGauge>();

    public sealed override ClassJobID[] JobIDs => new [] { ClassJobID.Bard, ClassJobID.Archer };


    /// <summary>
    /// ǿ�����
    /// </summary>
    public static BaseAction HeavyShoot { get; } = new(ActionID.HeavyShoot) { BuffsProvide = new[] { StatusID.StraightShotReady } };

    /// <summary>
    /// ֱ�����
    /// </summary>
    public static BaseAction StraitShoot { get; } = new(ActionID.StraitShoot) { BuffsNeed = new[] { StatusID.StraightShotReady } };

    /// <summary>
    /// ��ҧ��
    /// </summary>
    public static BaseAction VenomousBite { get; } = new(ActionID.VenomousBite, isEot: true)
    {
        TargetStatus = new[] { StatusID.VenomousBite, StatusID.CausticBite }
    };
    
    /// <summary>
    /// ��ʴ��
    /// </summary>
    public static BaseAction Windbite { get; } = new(ActionID.Windbite, isEot: true)
    {
        TargetStatus = new[] { StatusID.Windbite, StatusID.Stormbite }
    };

    /// <summary>
    /// ��������
    /// </summary>
    public static BaseAction IronJaws { get; } = new(ActionID.IronJaws, isEot: true);

    /// <summary>
    /// �������С������
    /// </summary>
    public static BaseAction WanderersMinuet { get; } = new(ActionID.WanderersMinuet);

    /// <summary>
    /// ���ߵ�����ҥ
    /// </summary>
    public static BaseAction MagesBallad { get; } = new(ActionID.MagesBallad);

    /// <summary>
    /// �����������
    /// </summary>
    public static BaseAction ArmysPaeon { get; } = new(ActionID.ArmysPaeon);

    /// <summary>
    /// ս��֮��
    /// </summary>
    public static BaseAction BattleVoice { get; } = new(ActionID.BattleVoice, true);

    /// <summary>
    /// ����ǿ��
    /// </summary>
    public static BaseAction RagingStrikes { get; } = new(ActionID.RagingStrikes, true);

    /// <summary>
    /// ���������������
    /// </summary>
    public static BaseAction RadiantFinale { get; } = new(ActionID.RadiantFinale, true)
    {
        OtherCheck = b => JobGauge.Coda.Any(s => s != Song.NONE),
    };

    /// <summary>
    /// ���Ҽ�
    /// </summary>
    public static BaseAction Barrage { get; } = new(ActionID.Barrage);

    /// <summary>
    /// ��������
    /// </summary>
    public static BaseAction EmpyrealArrow { get; } = new(ActionID.EmpyrealArrow);

    /// <summary>
    /// ��������
    /// </summary>
    public static BaseAction PitchPerfect { get; } = new(ActionID.PitchPerfect)
    {
        OtherCheck = b => JobGauge.Song == Song.WANDERER,
    };

    /// <summary>
    /// ʧѪ��
    /// </summary>
    public static BaseAction Bloodletter { get; } = new(ActionID.Bloodletter);

    /// <summary>
    /// ��������
    /// </summary>
    public static BaseAction RainofDeath { get; } = new(ActionID.RainofDeath);

    /// <summary>
    /// �����
    /// </summary>
    public static BaseAction QuickNock { get; } = new(ActionID.QuickNock)
    {
        BuffsProvide = new[] { StatusID.ShadowbiteReady }
    };

    /// <summary>
    /// Ӱ�ɼ�
    /// </summary>
    public static BaseAction Shadowbite { get; } = new(ActionID.Shadowbite)
    {
        BuffsNeed = new[] { StatusID.ShadowbiteReady }
    };

    /// <summary>
    /// ����������޿���
    /// </summary>
    public static BaseAction WardensPaean { get; } = new(ActionID.WardensPaean, true);

    /// <summary>
    /// ��������������
    /// </summary>
    public static BaseAction NaturesMinne { get; } = new(ActionID.NaturesMinne, true);

    /// <summary>
    /// ����յ���
    /// </summary>
    public static BaseAction Sidewinder { get; } = new(ActionID.Sidewinder);

    /// <summary>
    /// �����
    /// </summary>
    public static BaseAction ApexArrow { get; } = new(ActionID.ApexArrow)
    {
        OtherCheck = b => JobGauge.SoulVoice >= 20 || Player.HaveStatus(true, StatusID.BlastArrowReady),
    };

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction Troubadour { get; } = new(ActionID.Troubadour, true)
    {
        OtherCheck = b => !Player.HaveStatus(false, StatusID.Troubadour,
            StatusID.Tactician1,
            StatusID.Tactician2,
            StatusID.ShieldSamba),
    };

    private protected override bool EmergercyAbility(byte abilityRemain, IAction nextGCD, out IAction act)
    {
        //��ĳЩ�ǳ�Σ�յ�״̬��
        if (CommandController.EsunaOrShield && TargetUpdater.WeakenPeople.Length > 0 || TargetUpdater.DyingPeople.Length > 0)
        {
            if (WardensPaean.ShouldUse(out act, mustUse: true)) return true;
        }
        return base.EmergercyAbility(abilityRemain, nextGCD, out act);
    }
}