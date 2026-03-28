using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class SuperDummy : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Super Dummy");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}

		public override void SetDefaults()
		{
			NPC.width = 18;
			NPC.height = 48;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 9999999;
			NPC.HitSound = SoundID.NPCHit15;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 0f;
			NPC.knockBackResist = 0f;
		}

		public override void UpdateLifeRegen (ref int damage)
		{
			NPC.lifeRegen += 2000000;
		}

		public override bool? DrawHealthBar (byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}

		public override bool CheckDead ()
		{
			if (NPC.lifeRegen < 0)
			{
				NPC.life = NPC.lifeMax;
				return false;
			}
			return true;
		}
	}
}
