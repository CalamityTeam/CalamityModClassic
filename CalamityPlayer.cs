using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using CalamityModClassic1Point1.NPCs.TheDevourerofGods;
using CalamityModClassic1Point1.NPCs.Calamitas;
using CalamityModClassic1Point1.NPCs.PlaguebringerGoliath;
using CalamityModClassic1Point1.NPCs.Yharon;
using CalamityModClassic1Point1.NPCs.Leviathan;
using CalamityModClassic1Point1.Items.Armor;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace CalamityModClassic1Point1
{
	public class CalamityPlayer : ModPlayer
	{
		private const int saveVersion = 0;
		public float shieldInvinc = 5f;
		public int dashMod;
		public int dashTimeMod;
		public int dashDelayMod;
		public bool dodgeScarf = false;
		public bool cryogenSoul = false;
		public bool yInsignia = false;
		public bool tarraRegen = false;
		public bool tarraReach = false;
		public bool tarraCalm = false;
		public bool elysianAegis = false;
		public bool elysianGuard = false;
		public bool godSlayer = false;
		public bool godSlayerDamage = false;
		public bool godSlayerReflect = false;
		public bool godSlayerCooldown = false;
		public bool scarfCooldown = false;
		public bool flamethrowerBoost = false;
		public static bool shadowMinions = false;
		public static bool tearMinions = false;
		public static bool alchFlask = false;
		public static bool reaverBlast = false;
		public static float ataxiaDmg;
		public static bool ataxiaHurt = false;
		public static bool ataxiaHeal = false;
		public static float xerocDmg;
		public static bool xerocBlast = false; //melee
		public static bool xerocHurt = false; //magic
		public static bool xerocHeal = false; //magic heal
		public static bool xerocSpike = false; //ranged
		public static bool xerocTear = false; //thrown
		public static bool xerocSummon = false; //summon
		public bool IBoots = false;
		public bool elysianFire = false;
		public bool dsSetBonus = false;
		public bool bFlames = false;
		public bool pFlames = false;
		public bool hFlames = false;
		public bool gState = false;
		public bool bBlood = false;
		public bool rRage = false;
		public bool tRegen = false;
		public bool xRage = false;
		public bool xWrath = false;
		public bool graxDefense = false;
		public bool eGravity = false;
		public bool sMeleeBoost = false;
		public bool tFury = false;
		public bool vHex = false;
		public bool mWorm = false;
		public bool cEyes = false;
		public bool cSlime = false;
		public bool cSlime2 = false;
		public bool bStar = false;
		public bool SP = false;
		public bool dCreeper = false;
		public bool bClot = false;
		public bool eAxe = false;
		public bool SPG = false;
		public bool aChicken = false;
		public bool sWaifu = false;
		public bool dWaifu = false;
		public bool cWaifu = false;
		public bool ZoneCalamity = false;
		
		public override void ResetEffects()
		{
			dashMod = 0;
			dodgeScarf = false;
			elysianAegis = false;
			godSlayer = false;
			godSlayerDamage = false;
			godSlayerReflect = false;
			godSlayerCooldown = false;
			scarfCooldown = false;
			flamethrowerBoost = false;
			cryogenSoul = false;
			yInsignia = false;
			shadowMinions = false;
			tearMinions = false;
			alchFlask = false;
			reaverBlast = false;
			ataxiaHurt = false;
			ataxiaHeal = false;
			tarraRegen = false;
			tarraReach = false;
			tarraCalm = false;
			xerocBlast = false;
			xerocHurt = false;
			xerocSpike = false;
			xerocSummon = false;
			xerocTear = false;
			xerocHeal = false;
			IBoots = false;
			elysianFire = false;
			dsSetBonus = false;
			bFlames = false;
			pFlames = false;
			hFlames = false;
			gState = false;
			bBlood = false;
			rRage = false;
			xRage = false;
			xWrath = false;
			graxDefense = false;
			eGravity = false;
			sMeleeBoost = false;
			tFury = false;
			vHex = false;
			tRegen = false;
			mWorm = false;
			cEyes = false;
			cSlime = false;
			cSlime2 = false;
			bStar = false;
			SP = false;
			dCreeper = false;
			bClot = false;
			eAxe = false;
			SPG = false;
			aChicken = false;
			sWaifu = false;
			dWaifu = false;
			cWaifu = false;
		}

		public override void UpdateDead()
		{
			bFlames = false;
			pFlames = false;
			hFlames = false;
			gState = false;
			bBlood = false;
			rRage = false;
			xRage = false;
			xWrath = false;
			graxDefense = false;
			eGravity = false;
			sMeleeBoost = false;
			tFury = false;
			vHex = false;
			elysianAegis = false;
			elysianGuard = false;
			godSlayer = false;
			godSlayerDamage = false;
			godSlayerReflect = false;
			godSlayerCooldown = false;
			scarfCooldown = false;
			flamethrowerBoost = false;
			tRegen = false;
			IBoots = false;
			elysianFire = false;
			dsSetBonus = false;
			reaverBlast = false;
			dodgeScarf = false;
			cryogenSoul = false;
			yInsignia = false;
			shadowMinions = false;
			tearMinions = false;
			alchFlask = false;
			ataxiaHurt = false;
			ataxiaHeal = false;
			tarraCalm = false;
			tarraReach = false;
			tarraRegen = false;
			xerocTear = false;
			xerocSummon = false;
			xerocSpike = false;
			xerocHurt = false;
			xerocBlast = false;
			xerocHeal = false;
		}

		public override void UpdateBadLifeRegen()
		{
			if (bFlames)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 16;
			}
			if (hFlames)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 20;
			}
			if (pFlames)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 10;
				Player.blind = true;
				Player.statDefense -= 4;
				Player.moveSpeed -= 0.1f;
			}
			if (gState)
			{
				Player.statDefense /= 2;
				Player.velocity.Y = 0f;
				Player.velocity.X = 0f;
			}
			if (bBlood)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 8;
				Player.blind = true;
				Player.statDefense -= 3;
				Player.moveSpeed += 0.2f;
				Player.GetAttackSpeed(DamageClass.Melee) *= 1.025f;
				Player.GetDamage(DamageClass.Melee) *= 1.05f;
				Player.GetDamage(DamageClass.Ranged) *= 0.9f;
				Player.GetDamage(DamageClass.Magic) *= 0.9f;
			}
			if (rRage)
			{
				Player.GetDamage(DamageClass.Melee) *= 1.1f;
				Player.GetCritChance(DamageClass.Melee) += 6;
				Player.GetAttackSpeed(DamageClass.Melee) *= 1.05f;
        		Player.moveSpeed += 0.05f;
			}
			if (tRegen)
			{
				Player.lifeRegen += 10;
			}
			if (tarraCalm)
			{
				Player.calmed = true;
			}
			if (tarraReach)
			{
				Player.lifeMagnet = true;
			}
			if (xRage)
			{
				Player.GetDamage(DamageClass.Throwing) *= 1.1f;
				Player.GetDamage(DamageClass.Ranged) *= 1.1f;
				Player.GetDamage(DamageClass.Melee) *= 1.1f;
				Player.GetDamage(DamageClass.Magic) *= 1.1f;
				Player.GetDamage(DamageClass.Summon) *= 1.1f;
			}
			if (xWrath)
			{
				Player.GetCritChance(DamageClass.Melee) += 10;
				Player.GetCritChance(DamageClass.Magic) += 10;
				Player.GetCritChance(DamageClass.Ranged) += 10;
				Player.GetCritChance(DamageClass.Throwing) += 10;
			}
			if (godSlayerCooldown)
			{
				Player.GetDamage(DamageClass.Throwing) *= 1.1f;
				Player.GetDamage(DamageClass.Ranged) *= 1.1f;
				Player.GetDamage(DamageClass.Melee) *= 1.1f;
				Player.GetDamage(DamageClass.Magic) *= 1.1f;
				Player.GetDamage(DamageClass.Summon) *= 1.1f;
			}
			if (aChicken)
			{
				Player.lifeRegen += 5;
				Player.statDefense += 10;
				Player.moveSpeed += 0.1f;
			}
			if (graxDefense)
			{
				Player.statDefense += 80;
				Player.endurance += 0.25f;
				Player.GetAttackSpeed(DamageClass.Melee) *= 1.1f;
				Player.GetDamage(DamageClass.Melee) *= 1.25f;
				Player.GetCritChance(DamageClass.Melee) += 25;
			}
			if (eGravity)
			{
				if (Player.wingTimeMax <= 0)
				{
					Player.wingTimeMax = 0;
				}
				Player.wingTimeMax /= 4;
			}
			if (sMeleeBoost)
			{
				Player.GetAttackSpeed(DamageClass.Melee) *= 1.1f;
				Player.GetDamage(DamageClass.Melee) *= 1.1f;
				Player.GetCritChance(DamageClass.Melee) += 10;
			}
			if (tFury)
			{
				Player.GetDamage(DamageClass.Melee) *= 1.3f;
				Player.GetCritChance(DamageClass.Melee) += 30;
			}
			if (vHex)
			{
				Player.endurance -= 0.3f;
				Player.statDefense -= 30;
			}
		}
		
		public override void PostUpdateMiscEffects()
        {
            bool useNebula = NPC.AnyNPCs(Mod.Find<ModNPC>("DevourerofGodsHead").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point1:DevourerofGodsHead", useNebula);
            bool useBrimstone = NPC.AnyNPCs(Mod.Find<ModNPC>("CalamitasRun3").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point1:CalamitasRun3", useBrimstone);
            bool usePlague = NPC.AnyNPCs(Mod.Find<ModNPC>("PlaguebringerGoliath").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point1:PlaguebringerGoliath", usePlague);
            bool useFire = NPC.AnyNPCs(Mod.Find<ModNPC>("Yharon").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point1:Yharon", useFire);
            bool useWater = NPC.AnyNPCs(Mod.Find<ModNPC>("Leviathan").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point1:Leviathan", useWater);
            bool useHoly = NPC.AnyNPCs(Mod.Find<ModNPC>("Providence").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point1:Providence", useHoly);
            bool useSBrimstone = NPC.AnyNPCs(Mod.Find<ModNPC>("SupremeCalamitas").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point1:SupremeCalamitas", useSBrimstone);
            if (Player.pulley)
			{
				ModDashMovement();
			}
			else if (Player.grappling[0] == -1 && !Player.tongued)
			{
				ModHorizontalMovement();
				ModDashMovement();
			}
			if (Main.hasFocus)
			{
				for (int l = 0; l < 4; l++)
				{
					bool flag5 = false;
					bool flag6 = false;
					switch (l)
					{
					case 0:
						flag5 = (Player.controlDown && Player.releaseDown);
						flag6 = Player.controlDown;
						break;
					case 1:
						flag5 = (Player.controlUp && Player.releaseUp);
						flag6 = Player.controlUp;
						break;
					case 2:
						flag5 = (Player.controlRight && Player.releaseRight);
						flag6 = Player.controlRight;
						break;
					case 3:
						flag5 = (Player.controlLeft && Player.releaseLeft);
						flag6 = Player.controlLeft;
						break;
					}
					if (flag5)
					{
						if (Player.doubleTapCardinalTimer[l] > 0)
						{
							ModKeyDoubleTap(l);
						}
						else
						{
							Player.doubleTapCardinalTimer[l] = 15;
						}
					}
					if (flag6)
					{
						Player.holdDownCardinalTimer[l]++;
						Player.KeyHoldDown(l, Player.holdDownCardinalTimer[l]);
					}
					else
					{
						Player.holdDownCardinalTimer[l] = 0;
					}
				}
			}
			if (elysianAegis)
			{
				bool flag14 = false;
				if (elysianGuard)
				{
					float num29 = shieldInvinc;
					shieldInvinc -= 0.08f;
					if (shieldInvinc < 0f)
					{
						shieldInvinc = 0f;
					}
					else
					{
						flag14 = true;
					}
					if (shieldInvinc == 0f && num29 != shieldInvinc && Main.netMode == 1)
					{
						NetMessage.SendData(84, -1, -1, null, Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
					Player.GetDamage(DamageClass.Ranged) += (5f - shieldInvinc) * 0.04f;
					Player.GetCritChance(DamageClass.Ranged) += (int)((5f - shieldInvinc) * 4f);
					Player.GetDamage(DamageClass.Melee) += (5f - shieldInvinc) * 0.04f;
					Player.GetCritChance(DamageClass.Melee) += (int)((5f - shieldInvinc) * 4f);
					Player.GetDamage(DamageClass.Magic) += (5f - shieldInvinc) * 0.04f;
					Player.GetCritChance(DamageClass.Magic) += (int)((5f - shieldInvinc) * 4f);
					Player.GetDamage(DamageClass.Summon) += (5f - shieldInvinc) * 0.04f;
					Player.GetDamage(DamageClass.Throwing) += (5f - shieldInvinc) * 0.04f;
					Player.GetCritChance(DamageClass.Throwing) += (int)((5f - shieldInvinc) * 4f);
					Player.aggro += (int)((5f - shieldInvinc) * 220f);
					Player.statDefense += (int)((5f - shieldInvinc) * 15f);
					Player.moveSpeed *= 0.5f;
					if (Player.mount.Active)
					{
						elysianGuard = false;
					}
				}
				else
				{
					float num30 = shieldInvinc;
					shieldInvinc += 0.08f;
					if (shieldInvinc > 5f)
					{
						shieldInvinc = 5f;
					}
					else
					{
						flag14 = true;
					}
					if (shieldInvinc == 5f && num30 != shieldInvinc && Main.netMode == 1)
					{
						NetMessage.SendData(84, -1, -1, null, Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (flag14)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 vector = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust = Main.dust[Dust.NewDust(Player.Center - vector * 30f, 0, 0, 244, 0f, 0f, 0, default(Color), 1f)];
						dust.noGravity = true;
						dust.position = Player.Center - vector * (float)Main.rand.Next(5, 11);
						dust.velocity = vector.RotatedBy(1.5707963705062866, default(Vector2)) * 4f;
						dust.scale = 0.5f + Main.rand.NextFloat();
						dust.fadeIn = 0.5f;
					}
					if (Main.rand.Next(2) == 0)
					{
						Vector2 vector2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust2 = Main.dust[Dust.NewDust(Player.Center - vector2 * 30f, 0, 0, 246, 0f, 0f, 0, default(Color), 1f)];
						dust2.noGravity = true;
						dust2.position = Player.Center - vector2 * 12f;
						dust2.velocity = vector2.RotatedBy(-1.5707963705062866, default(Vector2)) * 2f;
						dust2.scale = 0.5f + Main.rand.NextFloat();
						dust2.fadeIn = 0.5f;
					}
				}
			}
			else
			{
				elysianGuard = false;
			}
		}
		
		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (godSlayer && !godSlayerCooldown)
			{
				SoundEngine.PlaySound(SoundID.Item67, Player.position);
				for (int j = 0; j < 100; j++)
				{
					int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 173, 0f, 0f, 100, default(Color), 2f);
					Dust expr_A4_cp_0 = Main.dust[num];
					expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
					Dust expr_CB_cp_0 = Main.dust[num];
					expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
					Main.dust[num].velocity *= 0.9f;
					Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					}
				}
				Player.statLife += 300;
    			Player.HealEffect(300);
    			Player.AddBuff(Mod.Find<ModBuff>("GodSlayerCooldown").Type, 1800);
				return false;
			}
			if (bBlood && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(" became a blood geyser");
			}
			if (bFlames && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(" was consumed by the black flames");
			}
			if (pFlames && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason("'s flesh was melted by the Plague");
			}
			if (hFlames && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(" fell prey to their sins");
			}
			return true;
		}

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (dodgeScarf && item.CountsAsClass(DamageClass.Melee) && item.shoot == 0)
            {
                damage *= 1.25f;
            }
            if (flamethrowerBoost && item.CountsAsClass(DamageClass.Ranged) && item.useAmmo == 23)
            {
				damage *= 1.25f;
            }
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
		{
			if (aChicken && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Main.rand.Next(3) == 0)
		        {
		        	int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 244, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
		        }
			}
			if (cryogenSoul && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Main.rand.Next(3) == 0)
		        {
		        	int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 67, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
		        }
			}
			if (xerocBlast && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Main.rand.Next(3) == 0)
		        {
		        	int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 58, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 1.25f);
		        }
			}
			if (reaverBlast && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Main.rand.Next(3) == 0)
		        {
		        	int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 74, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
		        }
			}
			if (dsSetBonus && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Main.rand.Next(3) == 0)
		        {
		        	int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 27, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 2.5f);
		        }
			}
		}
		
		public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
		{
			if (tarraRegen)
			{
				if (Main.rand.Next(12) == 0)
				{
					Player.AddBuff(Mod.Find<ModBuff>("TarraLifeRegen").Type, Main.rand.Next(120, 360));
				}
			}
			if (dsSetBonus)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(153, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(153, 240, false);
				}
				else
				{
					target.AddBuff(153, 120, false);
				}
			}
			if (cryogenSoul)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(44, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(44, 240, false);
				}
				else
				{
					target.AddBuff(44, 120, false);
				}
			}
			if (aChicken)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120, false);
				}
			}
			if (yInsignia)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120, false);
				}
			}
			if (alchFlask)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 120, false);
				}
			}
		}
		
		public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
		{
			if (attempt.common)
			{
				return;
			}
			if (Player.FindBuffIndex(BuffID.Gills) > -1 && Main.hardMode && !attempt.inLava && Main.rand.Next(5) == 0)
			{
				itemDrop = Mod.Find<ModItem>("Floodtide").Type;
			}
		}
		
		public void ScarfDodge()
		{
			Player.immune = true;
			Player.immuneTime = 80;
			if (Player.longInvince)
			{
				Player.immuneTime += 10;
			}
			for (int i = 0; i < Player.hurtCooldowns.Length; i++)
			{
				Player.hurtCooldowns[i] = Player.immuneTime;
			}
			for (int j = 0; j < 100; j++)
			{
				int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 235, 0f, 0f, 100, default(Color), 2f);
				Dust expr_A4_cp_0 = Main.dust[num];
				expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
				Dust expr_CB_cp_0 = Main.dust[num];
				expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
				Main.dust[num].velocity *= 0.4f;
				Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
				Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					Main.dust[num].noGravity = true;
				}
			}
			if (Player.whoAmI == Main.myPlayer)
			{
				NetMessage.SendData(62, -1, -1, null, Player.whoAmI, 1f, 0f, 0f, 0, 0, 0);
			}
		}

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (godSlayerDamage)
            {
                if (info.Damage > 80)
                {
                    SoundEngine.PlaySound(SoundID.Item73, Player.position);
                    float spread = 45f * 0.0174f;
                    double startAngle = Math.Atan2(Player.velocity.X, Player.velocity.Y) - spread / 2;
                    double deltaAngle = spread / 8f;
                    double offsetAngle;
                    int i;
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        for (i = 0; i < 4; i++)
                        {
                            offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("GodKiller").Type, 300, 5f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("GodKiller").Type, 300, 5f, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
                else if (info.Damage <= 80)
                {
					return true;
                }
            }
            if (godSlayerReflect && Main.rand.Next(20) == 0)
            {
				return true;
            }
            if (Player.whoAmI == Main.myPlayer && dodgeScarf && !scarfCooldown)
            {
                ScarfDodge();
                Player.AddBuff(Mod.Find<ModBuff>("ScarfMeleeBoost").Type, Main.rand.Next(360, 600));
                Player.AddBuff(Mod.Find<ModBuff>("ScarfCooldown").Type, 600);
				return true;
            }
			return false;
        }
		
		public void ModDashMovement()
		{
			if (dashMod == 4 && dashDelayMod < 0 && Player.whoAmI == Main.myPlayer)
			{
				Rectangle rectangle = new Rectangle((int)((double)Player.position.X + (double)Player.velocity.X * 0.5 - 4.0), (int)((double)Player.position.Y + (double)Player.velocity.Y * 0.5 - 4.0), Player.width + 8, Player.height + 8);
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].immune[Player.whoAmI] <= 0)
					{
						NPC nPC = Main.npc[i];
						Rectangle rect = nPC.getRect();
						if (rectangle.Intersects(rect) && (nPC.noTileCollide || Player.CanHit(nPC)))
						{
							float num = 1500f * Player.GetDamage(DamageClass.Melee).Flat;
							float num2 = 15f;
							bool crit = false;
							if (Player.kbGlove)
							{
								num2 *= 2f;
							}
							if (Player.kbBuff)
							{
								num2 *= 1.5f;
							}
							if (Main.rand.Next(100) < Player.GetCritChance(DamageClass.Melee))
							{
								crit = true;
							}
							int direction = Player.direction;
							if (Player.velocity.X < 0f)
							{
								direction = -1;
							}
							if (Player.velocity.X > 0f)
							{
								direction = 1;
							}
							if (Player.whoAmI == Main.myPlayer)
							{
								Player.ApplyDamageToNPC(nPC, (int)num, num2, direction, crit);
								int num6 = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyExplosionSupreme").Type, 1000, 20f, Main.myPlayer, 0f, 0f);
								Main.projectile[num6].Kill();
								Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyEruption").Type, 780, 5f, Main.myPlayer, 0f, 0f);
							}
							nPC.immune[Player.whoAmI] = 6;
							Player.immune = true;
							Player.immuneNoBlink = true;
							Player.immuneTime = 4;
						}
					}
				}
			}
			if (dashMod == 3 && dashDelayMod < 0 && Player.whoAmI == Main.myPlayer)
			{
				Rectangle rectangle = new Rectangle((int)((double)Player.position.X + (double)Player.velocity.X * 0.5 - 4.0), (int)((double)Player.position.Y + (double)Player.velocity.Y * 0.5 - 4.0), Player.width + 8, Player.height + 8);
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].immune[Player.whoAmI] <= 0)
					{
						NPC nPC = Main.npc[i];
						Rectangle rect = nPC.getRect();
						if (rectangle.Intersects(rect) && (nPC.noTileCollide || Player.CanHit(nPC)))
						{
							float num = 500f * Player.GetDamage(DamageClass.Melee).Flat;
							float num2 = 12f;
							bool crit = false;
							if (Player.kbGlove)
							{
								num2 *= 2f;
							}
							if (Player.kbBuff)
							{
								num2 *= 1.5f;
							}
							if (Main.rand.Next(100) < Player.GetCritChance(DamageClass.Melee))
							{
								crit = true;
							}
							int direction = Player.direction;
							if (Player.velocity.X < 0f)
							{
								direction = -1;
							}
							if (Player.velocity.X > 0f)
							{
								direction = 1;
							}
							if (Player.whoAmI == Main.myPlayer)
							{
								Player.ApplyDamageToNPC(nPC, (int)num, num2, direction, crit);
								int num6 = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyExplosionSupreme").Type, 500, 15f, Main.myPlayer, 0f, 0f);
								Main.projectile[num6].Kill();
								Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyEruption").Type, 380, 5f, Main.myPlayer, 0f, 0f);
							}
							nPC.immune[Player.whoAmI] = 6;
							Player.immune = true;
							Player.immuneNoBlink = true;
							Player.immuneTime = 4;
						}
					}
				}
			}
			if (dashMod == 2 && dashDelayMod < 0 && Player.whoAmI == Main.myPlayer)
			{
				Rectangle rectangle = new Rectangle((int)((double)Player.position.X + (double)Player.velocity.X * 0.5 - 4.0), (int)((double)Player.position.Y + (double)Player.velocity.Y * 0.5 - 4.0), Player.width + 8, Player.height + 8);
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].immune[Player.whoAmI] <= 0)
					{
						NPC nPC = Main.npc[i];
						Rectangle rect = nPC.getRect();
						if (rectangle.Intersects(rect) && (nPC.noTileCollide || Player.CanHit(nPC)))
						{
							float num = 100f * Player.GetDamage(DamageClass.Melee).Flat;
							float num2 = 9f;
							bool crit = false;
							if (Player.kbGlove)
							{
								num2 *= 2f;
							}
							if (Player.kbBuff)
							{
								num2 *= 1.5f;
							}
							if (Main.rand.Next(100) < Player.GetCritChance(DamageClass.Melee))
							{
								crit = true;
							}
							int direction = Player.direction;
							if (Player.velocity.X < 0f)
							{
								direction = -1;
							}
							if (Player.velocity.X > 0f)
							{
								direction = 1;
							}
							if (Player.whoAmI == Main.myPlayer)
							{
								Player.ApplyDamageToNPC(nPC, (int)num, num2, direction, crit);
								int num6 = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyExplosion").Type, 100, 15f, Main.myPlayer, 0f, 0f);
								Main.projectile[num6].Kill();
							}
							nPC.immune[Player.whoAmI] = 6;
							Player.immune = true;
							Player.immuneNoBlink = true;
							Player.immuneTime = 4;
						}
					}
				}
			}
			if (dashDelayMod > 0)
			{
				if (Player.eocDash > 0)
				{
					Player.eocDash--;
				}
				if (Player.eocDash == 0)
				{
					Player.eocHit = -1;
				}
				dashDelayMod--;
				return;
			}
			if (dashDelayMod < 0)
			{
				float num7 = 12f;
				float num8 = 0.992f;
				float num9 = Math.Max(Player.accRunSpeed, Player.maxRunSpeed);
				float num10 = 0.96f;
				int num11 = 20;
				if (dashMod == 1)
				{
					for (int k = 0; k < 2; k++)
					{
						int num12;
						if (Player.velocity.Y == 0f)
						{
							num12 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)Player.height - 4f), Player.width, 8, 235, 0f, 0f, 100, default(Color), 1.4f);
						}
						else
						{
							num12 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)(Player.height / 2) - 8f), Player.width, 16, 235, 0f, 0f, 100, default(Color), 1.4f);
						}
						Main.dust[num12].velocity *= 0.1f;
						Main.dust[num12].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
				}
				else if (dashMod == 2)
				{
					for (int m = 0; m < 4; m++)
					{
						int num14 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 4f), Player.width, Player.height - 8, 246, 0f, 0f, 100, default(Color), 2.75f);
						Main.dust[num14].velocity *= 0.1f;
						Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
						Main.dust[num14].noGravity = true;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num14].fadeIn = 0.5f;
						}
					}
					num7 = 14f;
					num8 = 0.985f;
					num10 = 0.94f;
					num11 = 20;
				}
				else if (dashMod == 3)
				{
					for (int m = 0; m < 12; m++)
					{
						int num14 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 4f), Player.width, Player.height - 8, 244, 0f, 0f, 100, default(Color), 2.75f);
						Main.dust[num14].velocity *= 0.1f;
						Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
						Main.dust[num14].noGravity = true;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num14].fadeIn = 0.5f;
						}
					}
					num7 = 16f;
					num8 = 0.98f;
					num10 = 0.92f;
					num11 = 20;
				}
				else if (dashMod == 4)
				{
					for (int m = 0; m < 24; m++)
					{
						int num14 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 4f), Player.width, Player.height - 8, 244, 0f, 0f, 100, default(Color), 2.75f);
						Main.dust[num14].velocity *= 0.1f;
						Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
						Main.dust[num14].noGravity = true;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num14].fadeIn = 0.5f;
						}
					}
					num7 = 18f;
					num8 = 0.976f;
					num10 = 0.9f;
					num11 = 20;
				}
				if (dashMod > 0)
				{
					Player.vortexStealthActive = false;
					if (Player.velocity.X > num7 || Player.velocity.X < -num7)
					{
						Player.velocity.X = Player.velocity.X * num8;
						return;
					}
					if (Player.velocity.X > num9 || Player.velocity.X < -num9)
					{
						Player.velocity.X = Player.velocity.X * num10;
						return;
					}
					dashDelayMod = num11;
					if (Player.velocity.X < 0f)
					{
						Player.velocity.X = -num9;
						return;
					}
					if (Player.velocity.X > 0f)
					{
						Player.velocity.X = num9;
						return;
					}
				}
			}
			else if (dashMod > 0 && !Player.mount.Active)
			{
				if (dashMod == 1)
				{
					int num16 = 0;
					bool flag = false;
					if (dashTimeMod > 0)
					{
						dashTimeMod--;
					}
					if (dashTimeMod < 0)
					{
						dashTimeMod++;
					}
					if (Player.controlRight && Player.releaseRight)
					{
						if (dashTimeMod > 0)
						{
							num16 = 1;
							flag = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = 15;
						}
					}
					else if (Player.controlLeft && Player.releaseLeft)
					{
						if (dashTimeMod < 0)
						{
							num16 = -1;
							flag = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = -15;
						}
					}
					if (flag)
					{
						Player.velocity.X = 16.9f * (float)num16;
						Point point = (Player.Center + new Vector2((float)(num16 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point2 = (Player.Center + new Vector2((float)(num16 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashDelayMod = -1;
						for (int num17 = 0; num17 < 20; num17++)
						{
							int num18 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 235, 0f, 0f, 100, default(Color), 2f);
							Dust expr_CDB_cp_0 = Main.dust[num18];
							expr_CDB_cp_0.position.X = expr_CDB_cp_0.position.X + (float)Main.rand.Next(-5, 6);
							Dust expr_D02_cp_0 = Main.dust[num18];
							expr_D02_cp_0.position.Y = expr_D02_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num18].velocity *= 0.2f;
							Main.dust[num18].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
							Main.dust[num18].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
						}
						return;
					}
				}
				else if (dashMod == 2)
				{
					int num23 = 0;
					bool flag3 = false;
					if (dashTimeMod > 0)
					{
						dashTimeMod--;
					}
					if (dashTimeMod < 0)
					{
						dashTimeMod++;
					}
					if (Player.controlRight && Player.releaseRight)
					{
						if (dashTimeMod > 0)
						{
							num23 = 1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = 15;
						}
					}
					else if (Player.controlLeft && Player.releaseLeft)
					{
						if (dashTimeMod < 0)
						{
							num23 = -1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = -15;
						}
					}
					if (flag3)
					{
						Player.velocity.X = 21.9f * (float)num23;
						Point point5 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point6 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashDelayMod = -1;
						for (int num24 = 0; num24 < 20; num24++)
						{
							int num25 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 246, 0f, 0f, 100, default(Color), 3f);
							Dust expr_13AF_cp_0 = Main.dust[num25];
							expr_13AF_cp_0.position.X = expr_13AF_cp_0.position.X + (float)Main.rand.Next(-5, 6);
							Dust expr_13D6_cp_0 = Main.dust[num25];
							expr_13D6_cp_0.position.Y = expr_13D6_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num25].velocity *= 0.2f;
							Main.dust[num25].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
							Main.dust[num25].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
							Main.dust[num25].noGravity = true;
							Main.dust[num25].fadeIn = 0.5f;
						}
					}
				}
				else if (dashMod == 3)
				{
					int num23 = 0;
					bool flag3 = false;
					if (dashTimeMod > 0)
					{
						dashTimeMod--;
					}
					if (dashTimeMod < 0)
					{
						dashTimeMod++;
					}
					if (Player.controlRight && Player.releaseRight)
					{
						if (dashTimeMod > 0)
						{
							num23 = 1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = 15;
						}
					}
					else if (Player.controlLeft && Player.releaseLeft)
					{
						if (dashTimeMod < 0)
						{
							num23 = -1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = -15;
						}
					}
					if (flag3)
					{
						Player.velocity.X = 26.9f * (float)num23;
						Point point5 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point6 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashDelayMod = -1;
						for (int num24 = 0; num24 < 40; num24++)
						{
							int num25 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 244, 0f, 0f, 100, default(Color), 3f);
							Dust expr_13AF_cp_0 = Main.dust[num25];
							expr_13AF_cp_0.position.X = expr_13AF_cp_0.position.X + (float)Main.rand.Next(-5, 6);
							Dust expr_13D6_cp_0 = Main.dust[num25];
							expr_13D6_cp_0.position.Y = expr_13D6_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num25].velocity *= 0.2f;
							Main.dust[num25].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
							Main.dust[num25].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
							Main.dust[num25].noGravity = true;
							Main.dust[num25].fadeIn = 0.5f;
						}
					}
				}
				else if (dashMod == 4)
				{
					int num23 = 0;
					bool flag3 = false;
					if (dashTimeMod > 0)
					{
						dashTimeMod--;
					}
					if (dashTimeMod < 0)
					{
						dashTimeMod++;
					}
					if (Player.controlRight && Player.releaseRight)
					{
						if (dashTimeMod > 0)
						{
							num23 = 1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = 15;
						}
					}
					else if (Player.controlLeft && Player.releaseLeft)
					{
						if (dashTimeMod < 0)
						{
							num23 = -1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = -15;
						}
					}
					if (flag3)
					{
						Player.velocity.X = 31.9f * (float)num23;
						Point point5 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point6 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashDelayMod = -1;
						for (int num24 = 0; num24 < 60; num24++)
						{
							int num25 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 244, 0f, 0f, 100, default(Color), 3f);
							Dust expr_13AF_cp_0 = Main.dust[num25];
							expr_13AF_cp_0.position.X = expr_13AF_cp_0.position.X + (float)Main.rand.Next(-5, 6);
							Dust expr_13D6_cp_0 = Main.dust[num25];
							expr_13D6_cp_0.position.Y = expr_13D6_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num25].velocity *= 0.2f;
							Main.dust[num25].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
							Main.dust[num25].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
							Main.dust[num25].noGravity = true;
							Main.dust[num25].fadeIn = 0.5f;
						}
					}
				}
			}
		}
		
		public void ModHorizontalMovement()
		{
			float num = (Player.accRunSpeed + Player.maxRunSpeed) / 2f;
			if (Player.controlLeft && Player.velocity.X > -Player.accRunSpeed && dashDelayMod >= 0)
			{
				if (Player.mount.Active && Player.mount.Cart)
				{
					if (Player.velocity.X < 0f)
					{
						Player.direction = -1;
					}
				}
				else if ((Player.itemAnimation == 0 || Player.inventory[Player.selectedItem].useTurn) && Player.mount.AllowDirectionChange)
				{
					Player.direction = -1;
				}
				if (Player.velocity.Y == 0f || Player.wingsLogic > 0 || Player.mount.CanFly())
				{
					if (Player.velocity.X > Player.runSlowdown)
					{
						Player.velocity.X = Player.velocity.X - Player.runSlowdown;
					}
					Player.velocity.X = Player.velocity.X - Player.runAcceleration * 0.2f;
					if (Player.wingsLogic > 0)
					{
						Player.velocity.X = Player.velocity.X - Player.runAcceleration * 0.2f;
					}
				}
				if (Player.onWrongGround)
				{
					if (Player.velocity.X < Player.runSlowdown)
					{
						Player.velocity.X = Player.velocity.X + Player.runSlowdown;
					}
					else
					{
						Player.velocity.X = 0f;
					}
				}
				if (Player.velocity.X < -num && Player.velocity.Y == 0f && !Player.mount.Active)
				{
					int num3 = 0;
					if (Player.gravDir == -1f)
					{
						num3 -= Player.height;
					}
					if (Player.runSoundDelay == 0 && Player.velocity.Y == 0f)
					{
						Player.runSoundDelay = Player.hermesStepSound.IntendedCooldown;
					}
					if (dashMod == 1)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, 235, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 1.5f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 2)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, 246, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 2.5f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 3)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, 244, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 4)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, 244, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
				}
			}
			else if (Player.controlRight && Player.velocity.X < Player.accRunSpeed && dashDelayMod >= 0)
			{
				if (Player.mount.Active && Player.mount.Cart)
				{
					if (Player.velocity.X > 0f)
					{
						Player.direction = -1;
					}
				}
				else if ((Player.itemAnimation == 0 || Player.inventory[Player.selectedItem].useTurn) && Player.mount.AllowDirectionChange)
				{
					Player.direction = 1;
				}
				if (Player.velocity.Y == 0f || Player.wingsLogic > 0 || Player.mount.CanFly())
				{
					if (Player.velocity.X < -Player.runSlowdown)
					{
						Player.velocity.X = Player.velocity.X + Player.runSlowdown;
					}
					Player.velocity.X = Player.velocity.X + Player.runAcceleration * 0.2f;
					if (Player.wingsLogic > 0)
					{
						Player.velocity.X = Player.velocity.X + Player.runAcceleration * 0.2f;
					}
				}
				if (Player.onWrongGround)
				{
					if (Player.velocity.X > Player.runSlowdown)
					{
						Player.velocity.X = Player.velocity.X - Player.runSlowdown;
					}
					else
					{
						Player.velocity.X = 0f;
					}
				}
				if (Player.velocity.X > num && Player.velocity.Y == 0f && !Player.mount.Active)
				{
					int num8 = 0;
					if (Player.gravDir == -1f)
					{
						num8 -= Player.height;
					}
					if (Player.runSoundDelay == 0 && Player.velocity.Y == 0f)
					{
						Player.runSoundDelay = Player.hermesStepSound.IntendedCooldown;
					}
					if (dashMod == 1)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, 235, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 1.5f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 2)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, 246, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 2.5f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 3)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, 244, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 4)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, 244, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
				}
			}
		}
		
		public void ModKeyDoubleTap(int keyDir)
		{
			int num = 0;
			if (Main.ReversedUpDownArmorSetBonuses)
			{
				num = 1;
			}
			if (keyDir == num)
			{
				if (elysianAegis && !Player.mount.Active)
				{
					elysianGuard = !elysianGuard;
				}
			}
		}

		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (tRegen)
			{
				if (Main.rand.Next(100) == 0)
				{
					Vector2 value = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
					value.Normalize();
					value.X *= 0.66f;
					int num36 = Gore.NewGore(Player.GetSource_FromThis(), drawInfo.Position + new Vector2((float)Main.rand.Next(Player.width + 1), (float)Main.rand.Next(Player.height + 1)), value * (float)Main.rand.Next(3, 6) * 0.33f, ModContent.Find<ModGore>("TarraHeart").Type, (float)Main.rand.Next(40, 121) * 0.01f);
					Main.gore[num36].sticky = false;
					Main.gore[num36].velocity *= 0.4f;
					Main.gore[num36].timeLeft = 60;
					Gore expr_2481_cp_0 = Main.gore[num36];
					expr_2481_cp_0.velocity.Y = expr_2481_cp_0.velocity.Y - 0.6f;
					drawInfo.GoreCache.Add(num36);
				}
				r *= 0.025f;
				g *= 0.15f;
				b *= 0.035f;
				fullBright = true;
			}
			if (IBoots)
			{
				if (((double)Player.velocity.X > 0 || (double)Player.velocity.Y > 0 || (double)Player.velocity.X < -0.1 || (double)Player.velocity.Y < -0.1) && !Player.mount.Active)
				{	
					if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
					{
						int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 229, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1f);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].velocity *= 2f;
						Main.dust[dust].velocity.Y *= 1.5f;
						drawInfo.DustCache.Add(dust);
					}
					r *= 0.05f;
					g *= 0.05f;
					b *= 0.05f;
					fullBright = true;
				}
			}
			if (elysianFire)
			{
				if (((double)Player.velocity.X > 0 || (double)Player.velocity.Y > 0 || (double)Player.velocity.X < -0.1 || (double)Player.velocity.Y < -0.1) && !Player.mount.Active)
				{	
					if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
					{
						int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 246, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1f);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].velocity *= 2f;
						Main.dust[dust].velocity.Y *= 1.5f;
                        drawInfo.DustCache.Add(dust);
					}
					r *= 0.75f;
					g *= 0.55f;
					b *= 0f;
					fullBright = true;
				}
			}
			if (dsSetBonus)
			{
				if (((double)Player.velocity.X > 0 || (double)Player.velocity.Y > 0 || (double)Player.velocity.X < -0.1 || (double)Player.velocity.Y < -0.1) && !Player.mount.Active)
				{	
					if (Main.rand.Next(1) == 0 && drawInfo.shadow == 0f)
					{
						int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 27, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1.5f);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].velocity *= 1.2f;
						Main.dust[dust].velocity.Y -= 0.15f;
                        drawInfo.DustCache.Add(dust);
					}
					r *= 0.15f;
					g *= 0.025f;
					b *= 0.1f;
					fullBright = true;
				}
			}
			if (bFlames)
			{
				if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, Mod.Find<ModDust>("BrimstoneFlame").Type, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
				}
				r *= 0.25f;
				g *= 0.01f;
				b *= 0.01f;
				fullBright = true;
			}
			if (hFlames)
			{
				if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, Mod.Find<ModDust>("HolyFlame").Type, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
				}
				r *= 0.25f;
				g *= 0.25f;
				b *= 0.1f;
				fullBright = true;
			}
			if (pFlames)
			{
				if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 89, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.2f;
					Main.dust[dust].velocity.Y -= 0.15f;
                    drawInfo.DustCache.Add(dust);
				}
				r *= 0.07f;
				g *= 0.15f;
				b *= 0.01f;
				fullBright = true;
			}
			if (gState)
			{
				r *= 0f;
				g *= 0.05f;
				b *= 0.3f;
				fullBright = true;
			}
			if (bBlood)
			{
				if (Main.rand.Next(6) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 5, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
				}
				r *= 0.15f;
				g *= 0.01f;
				b *= 0.01f;
				fullBright = true;
			}
		}
	}
}
