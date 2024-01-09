using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.NPCs
{
	public class CalamityGlobalNPC : GlobalNPC
	{
        public override bool InstancePerEntity => true; 
        public bool bFlames;
		public override void ResetEffects(NPC npc)
		{
            bFlames = false;
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (bFlames)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 20;
				if (damage < 3)
				{
					damage = 3;
				}
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (bFlames)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("BrimstoneFlame").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.Next(4) == 0)
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(npc.position, 0.05f, 0.01f, 0.01f);
			}
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.WyvernHead)
            {
                npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("WyvernFeather").Type, 2));
            }
            if (npc.type == NPCID.Vulture)
            {
                npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DesertFeather").Type, 2));
            }
            if (npc.type == NPCID.Golem)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("EnergyOrb").Type, 1, 6, 8));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("EnergyOrb").Type, 1, 4, 6));
            }
            if (npc.type == NPCID.Plantera)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("LivingShard").Type, 1, 6, 8));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("LivingShard").Type, 1, 4, 6));
            }
            if (npc.type == NPCID.RedDevil || npc.type == NPCID.SeekerHead || npc.type == NPCID.IchorSticker)
            {
                npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("EssenceofChaos").Type, 2));
            }
            if (npc.type == NPCID.WyvernHead || npc.type == NPCID.AngryNimbus)
            {
                npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("EssenceofCinder").Type, 2));
            }
            if (npc.type == NPCID.IceTortoise || npc.type == NPCID.IcyMerman)
            {
                npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("EssenceofEleum").Type, 2));
            }
            if (npc.type == NPCID.MossHornet || npc.type == NPCID.Derpling || npc.type == NPCID.GiantTortoise)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("LivingDewDroplet").Type, 3, 2, 3));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("LivingDewDroplet").Type, 3, 1, 2));
            }
            if (npc.type == NPCID.NebulaBrain || npc.type == NPCID.NebulaSoldier || npc.type == NPCID.NebulaHeadcrab || npc.type == NPCID.NebulaBeast)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("MeldBlob").Type, 4, 6, 7));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("MeldBlob").Type, 4, 3, 4));
            }
            if (npc.type == NPCID.CultistBoss)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("MeldBlob").Type, 1, 24, 28));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("MeldBlob").Type, 1, 20, 24));
            }
            if (npc.type == NPCID.RedDevil || npc.type == NPCID.Lavabat)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("TwistedTendril").Type, 2, 2, 2));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("TwistedTendril").Type, 2, 1, 1));
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("TwistedTendril").Type, 1, 5, 7));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("TwistedTendril").Type, 1, 3, 4));
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("VictoryShard").Type, 1, 3, 3));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("VictoryShard").Type, 1, 2, 2));
            }
        }
    }
}