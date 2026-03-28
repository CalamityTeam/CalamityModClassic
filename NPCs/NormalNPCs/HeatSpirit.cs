using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class HeatSpirit : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Heat Spirit");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
				new FlavorTextBestiaryInfoElement("Once a human that	lost their mind to the crags, now it seeks to eliminate any signs of foreign life within its new home.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.5f;
			NPC.aiStyle = 86;
			NPC.damage = 33;
			NPC.width = 40; //324
			NPC.height = 24; //216
			NPC.defense = 20;
			NPC.lifeMax = 50;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 1, 0);
			NPC.HitSound = SoundID.NPCHit52;
			NPC.DeathSound = SoundID.NPCDeath55;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("HeatSpiritBanner").Type;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || !Main.hardMode)
			{
				return 0f;
			}
			return SpawnCondition.Underworld.Chance * 0.065f;
		}
		
		public override void FindFrame(int frameHeight)
        {
			if (NPC.velocity.X < 0f)
			{
				NPC.direction = -1;
			}
			else
			{
				NPC.direction = 1;
			}
			if (NPC.direction == 1)
			{
				NPC.spriteDirection = 1;
			}
			if (NPC.direction == -1)
			{
				NPC.spriteDirection = -1;
			}
			NPC.rotation = (float)Math.Atan2((double)(NPC.velocity.Y * (float)NPC.direction), (double)(NPC.velocity.X * (float)NPC.direction));
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.5f, 0f, 0.05f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.OnFire, 120, true);
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 120, true);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("EssenceofChaos").Type, 4));
		}
	}
}