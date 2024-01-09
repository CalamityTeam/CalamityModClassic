using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point0.NPCs.SlimeGod
{
	public class SlimeGodRun : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
        public override void SetDefaults()
		{
			//NPC.name = "Crimson Slime God");
			//DisplayName.SetDefault("The Slime God");
			NPC.aiStyle = 15;
			NPC.damage = 110;
			NPC.width = 162;
			NPC.height = 108;
			NPC.defense = 20;
			NPC.lifeMax = 4000;
			NPC.knockBackResist = 0f;
			NPC.boss = true;
			AnimationType = 50;
			Main.npcFrameCount[NPC.type] = 6;
			NPC.value = Item.buyPrice(0, 10, 0, 0);
			NPC.alpha = 100;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound= SoundID.NPCHit1;
			NPC.DeathSound= SoundID.NPCDeath1;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			Music = MusicID.Boss1;
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Gel, 1, 100, 249));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("PurifiedGel").Type, 1, 15, 29));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("AbyssalTome").Type, 5));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("GelDart").Type, 1, 15, 29));
			npcLoot.Add(ItemDropRule.BossBag(Mod.Find<ModItem>("SlimeGodBag").Type));
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < hit.Damage / NPC.lifeMax * 100.0; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= (NPC.lifeMax * 0.75f) && NPC.life >= (NPC.lifeMax * 0.65f))
			{
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_OnHit(NPC), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("SlimeSpawnCrimson").Type, Main.rand.Next(1, 2));
			}
			if (NPC.life <= (NPC.lifeMax * 0.25f) && NPC.life >= (NPC.lifeMax * 0.15f))
			{
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_OnHit(NPC), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("SlimeSpawnCrimson").Type, Main.rand.Next(2, 3));
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void AI()
		{
			if (NPC.localAI[0] == 0f)
			{
				SoundEngine.PlaySound(SoundID.Roar, NPC.position);
				NPC.localAI[0] = 1f;
			}
			if (NPC.timeLeft > 10)
			{
				NPC.timeLeft = 10;
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode || Main.rand.Next(1) == 0)
			{
				target.AddBuff(BuffID.ManaSickness, 1600, true);
				target.AddBuff(BuffID.Cursed, 100, true);
			}
		}
	}
}