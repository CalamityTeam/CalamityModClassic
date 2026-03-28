using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class PlaguedTortoise : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plagueshell");
			Main.npcFrameCount[NPC.type] = 8;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				new FlavorTextBestiaryInfoElement("A tortoise overtaken by plague nanomachines, it now serves the plague completely.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 2f;
			NPC.damage = 80;
			NPC.aiStyle = 39;
			NPC.width = 46;
			NPC.height = 32;
			NPC.defense = 48;
			NPC.lifeMax = 800;
			NPC.knockBackResist = 0.2f;
			AnimationType = 153;
			NPC.value = Item.buyPrice(0, 0, 20, 0);
			NPC.HitSound = SoundID.NPCHit24;
			NPC.DeathSound = SoundID.NPCDeath27;
			NPC.noGravity = false;
            NPC.buffImmune[189] = true;
            NPC.buffImmune[153] = true;
            NPC.buffImmune[70] = true;
            NPC.buffImmune[69] = true;
            NPC.buffImmune[44] = true;
            NPC.buffImmune[39] = true;
            NPC.buffImmune[24] = true;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
            NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
            NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("PlagueshellBanner").Type;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerSafe || !NPC.downedGolemBoss || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
            {
                return 0f;
            }
            return SpawnCondition.HardmodeJungle.Chance * 0.09f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
	        target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 300, true);
		}
        
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
	        npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("PlagueCellCluster").Type, 1, 3, 5));
        }
	}
}