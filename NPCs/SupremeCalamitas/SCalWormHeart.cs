using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.SupremeCalamitas
{
	public class SCalWormHeart : ModNPC
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brimstone Heart");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 0; //70
			NPC.width = 24; //324
			NPC.height = 24; //216
			NPC.defense = 0;
            NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 180000 : 160000;
            if (CalamityWorldPreTrailer.death)
            {
                NPC.lifeMax = 240000;
            }
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = CalamityWorldPreTrailer.death ? 120000 : 90000;
            }
            NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.alpha = 255;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit13;
			NPC.DeathSound = SoundID.NPCDeath1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
		}

        public override void AI()
        {
            if (!Main.npc[CalamityGlobalNPC.SCal].active)
            {
                NPC.active = false;
                NPC.netUpdate = true;
                return;
            }
            NPC.alpha -= 42;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.type == Mod.Find<ModProjectile>("Celestus2").Type)
            {
                projectile.damage = (int)((double)projectile.damage * 0.66);
            }
        }

        public override bool CheckActive()
		{
			return false;
		}
		
		public override bool PreKill()
		{
			return false;
		}
	}
}