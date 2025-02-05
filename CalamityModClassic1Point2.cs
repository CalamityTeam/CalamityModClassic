using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.GameContent;
using Terraria.ModLoader;
using CalamityModClassic1Point2.NPCs.TheDevourerofGods;
using CalamityModClassic1Point2.NPCs.Calamitas;
using CalamityModClassic1Point2.NPCs.PlaguebringerGoliath;
using CalamityModClassic1Point2.NPCs.Yharon;
using CalamityModClassic1Point2.NPCs.Leviathan;
using CalamityModClassic1Point2.NPCs.Providence;
using CalamityModClassic1Point2.NPCs.SupremeCalamitas;
using CalamityModClassic1Point2.NPCs.Polterghast;
using CalamityModClassic1Point2.Tiles;
using CalamityModClassic1Point2.NPCs.Astrageldon;
using CalamityModClassic1Point2.NPCs.DesertScourge;
using CalamityModClassic1Point2.Items.DesertScourge;
using CalamityModClassic1Point2.NPCs.Crabulon;
using CalamityModClassic1Point2.Items.Crabulon;
using CalamityModClassic1Point2.NPCs.HiveMind;
using CalamityModClassic1Point2.Items.HiveMind;
using CalamityModClassic1Point2.NPCs.Perforator;
using CalamityModClassic1Point2.Items.Perforator;
using CalamityModClassic1Point2.NPCs.SlimeGod;
using CalamityModClassic1Point2.Items.SlimeGod;
using CalamityModClassic1Point2.NPCs.Cryogen;
using CalamityModClassic1Point2.Items.Cryogen;
using CalamityModClassic1Point2.NPCs.BrimstoneWaifu;
using CalamityModClassic1Point2.Items.BrimstoneWaifu;
using CalamityModClassic1Point2.Items.Calamitas;
using CalamityModClassic1Point2.NPCs.AstrumDeus;
using CalamityModClassic1Point2.Items.AstrumDeus;
using CalamityModClassic1Point2.Items.PlaguebringerGoliath;
using CalamityModClassic1Point2.NPCs.Scavenger;
using CalamityModClassic1Point2.Items.Scavenger;
using CalamityModClassic1Point2.NPCs.ProfanedGuardianBoss;
using CalamityModClassic1Point2.Items.ProfanedGuardian;
using CalamityModClassic1Point2.Items;
using CalamityModClassic1Point2.NPCs.CeaselessVoid;
using CalamityModClassic1Point2.Items.DevourerMunsters;
using CalamityModClassic1Point2.NPCs.StormWeaver;
using CalamityModClassic1Point2.NPCs.CosmicWraith;
using CalamityModClassic1Point2.Items.TheDevourerofGods;
using CalamityModClassic1Point2.NPCs.Bumblefuck;
using CalamityModClassic1Point2.Items.Bumblefuck;
using CalamityModClassic1Point2.Items.Yharon;
using CalamityModClassic1Point2.Items.SupremeCalamitas;
using CalamityModClassic1Point2.Items.Providence;

namespace CalamityModClassic1Point2
{
    [Autoload]
    public class CalamityModClassic1Point2 : Mod
    {
		public static Mod instance;
    	public static int ghostKillCount = 0;
    	
    	public override void Load()
		{
			instance = this;
			if (!Main.dedServ)
			{
				Filters.Scene["CalamityModClassic1Point2:DevourerofGodsHead"] = new Filter(new DoGScreenShaderData("FilterMiniTower").UseColor(0.4f, 0.1f, 1.0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point2:DevourerofGodsHead"] = new DoGSky();
				
				Filters.Scene["CalamityModClassic1Point2:CalamitasRun3"] = new Filter(new CalScreenShaderData("FilterMiniTower").UseColor(1.1f, 0.3f, 0.3f).UseOpacity(0.6f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point2:CalamitasRun3"] = new CalSky();
				
				Filters.Scene["CalamityModClassic1Point2:PlaguebringerGoliath"] = new Filter(new PbGScreenShaderData("FilterMiniTower").UseColor(0.3f, 0.9f, 0.2f).UseOpacity(0.65f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point2:PlaguebringerGoliath"] = new PbGSky();
				
				Filters.Scene["CalamityModClassic1Point2:Yharon"] = new Filter(new YScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.75f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point2:Yharon"] = new YSky();
				
				Filters.Scene["CalamityModClassic1Point2:Leviathan"] = new Filter(new LevScreenShaderData("FilterMiniTower").UseColor(0f, 0f, 0.5f).UseOpacity(0.5f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point2:Leviathan"] = new LevSky();
				
				Filters.Scene["CalamityModClassic1Point2:Providence"] = new Filter(new ProvScreenShaderData("FilterMiniTower").UseColor(0.6f, 0.45f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point2:Providence"] = new ProvSky();
				
				Filters.Scene["CalamityModClassic1Point2:SupremeCalamitas"] = new Filter(new SCalScreenShaderData("FilterMiniTower").UseColor(1.1f, 0.3f, 0.3f).UseOpacity(0.65f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point2:SupremeCalamitas"] = new SCalSky();
				
				Mod mod = ModLoader.GetMod("CalamityModClassic1Point2");
				UIHandler.OnLoad(mod);
			}			
		}
    	
    	public override void PostSetupContent()
        {
    		Mod mod = ModLoader.GetMod("CalamityModClassic1Point2");
            if(ModLoader.HasMod("BossChecklist"))
            {
                Mod bossChecklist = ModLoader.GetMod("BossChecklist");

				bossChecklist.Call(
				"LogBoss",
				instance,
				"DesertScourge",
				1.6f,
				() => CalamityWorld1Point2.downedDesertScourge,
				ModContent.NPCType<DesertScourgeHead>(),
				new Dictionary<string, object>()
				{
					["spawnItems"] = ModContent.ItemType<DriedSeafood>(),
					["spawnInfo"] = GetLocalization("NPCs.DesertScourgeHead.SpawnInfo")
				});
				bossChecklist.Call(
				"LogBoss",
				instance,
				"Crabulon",
				2.7f,
				() => CalamityWorld1Point2.downedCrabulon,
				ModContent.NPCType<CrabulonIdle>(),
				new Dictionary<string, object>()
				{
					["spawnItems"] = ModContent.ItemType<DecapoditaSprout>(),
					["spawnInfo"] = GetLocalization("NPCs.CrabulonIdle.SpawnInfo")
				});
				bossChecklist.Call(
				"LogBoss",
				instance,
				"HiveMind",
				3.98f,
				() => CalamityWorld1Point2.downedHiveMind,
				ModContent.NPCType<HiveMind>(),
				new Dictionary<string, object>()
				{
					["spawnItems"] = ModContent.ItemType<Teratoma>(),
					["spawnInfo"] = GetLocalization("NPCs.HiveMind.SpawnInfo")
				});
                bossChecklist.Call(
                "LogBoss",
                instance,
                "Perforator",
                3.99f,
                () => CalamityWorld1Point2.downedPerforator,
                ModContent.NPCType<PerforatorHive>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<BloodyWormFood>(),
                    ["spawnInfo"] = GetLocalization("NPCs.PerforatorHive.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "SlimeGod",
                6.7f,
                () => CalamityWorld1Point2.downedSlimeGod,
                ModContent.NPCType<SlimeGodCore>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<OverloadedSludge>(),
                    ["spawnInfo"] = GetLocalization("NPCs.SlimeGodCore.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "Cryogen",
                8.5f,
                () => CalamityWorld1Point2.downedCryogen,
                ModContent.NPCType<Cryogen>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<CryoKey>(),
                    ["spawnInfo"] = GetLocalization("NPCs.Cryogen.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "BrimstoneElemental",
                9.5f,
                () => CalamityWorld1Point2.downedBrimstoneElemental,
                ModContent.NPCType<BrimstoneElemental>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<CharredIdol>(),
                    ["spawnInfo"] = GetLocalization("NPCs.BrimstoneElemental.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "Calamitas",
                11.7f,
                () => CalamityWorld1Point2.downedCalamitas,
                ModContent.NPCType<Calamitas>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<BlightedEyeball>(),
                    ["spawnInfo"] = GetLocalization("NPCs.Calamitas.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "Leviathan",
                12.8f,
                () => CalamityWorld1Point2.downedLeviathan,
                ModContent.NPCType<Leviathan>(),
                new Dictionary<string, object>()
                {
                    ["spawnInfo"] = GetLocalization("NPCs.Leviathan.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "AstrumDeus",
                12.81f,
                () => CalamityWorld1Point2.downedStarGod,
                ModContent.NPCType<AstrumDeusHead>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<Starcore>(),
                    ["spawnInfo"] = GetLocalization("NPCs.AstrumDeusHead.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "PlaguebringerGoliath",
                13.54f,
                () => CalamityWorld1Point2.downedPlaguebringer,
                ModContent.NPCType<PlaguebringerGoliath>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<Abomination>(),
                    ["spawnInfo"] = GetLocalization("NPCs.PlaguebringerGoliath.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "Ravager",
                14.5f,
                () => CalamityWorld1Point2.downedScavenger,
                ModContent.NPCType<ScavengerBody>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<AncientMedallion>(),
                    ["spawnInfo"] = GetLocalization("NPCs.ScavengerBody.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "ProfanedGuardians",
                18.5f,
                () => CalamityWorld1Point2.downedGuardians,
                ModContent.NPCType<ProfanedGuardianBoss>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<ProfanedShard>(),
                    ["spawnInfo"] = GetLocalization("NPCs.ProfanedGuardianBoss.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "Providence",
                19f,
                () => CalamityWorld1Point2.downedProvidence,
                ModContent.NPCType<Providence>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<ProfanedCore>(),
                    ["spawnInfo"] = GetLocalization("NPCs.Providence.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "CeaselessVoid",
                19.1f,
                () => CalamityWorld1Point2.downedSentinel1,
                ModContent.NPCType<CeaselessVoid>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<RuneofCos>(),
                    ["spawnInfo"] = GetLocalization("NPCs.CeaselessVoid.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "StormWeaver",
                19.2f,
                () => CalamityWorld1Point2.downedSentinel2,
                ModContent.NPCType<StormWeaverHead>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<RuneofCos>(),
                    ["spawnInfo"] = GetLocalization("NPCs.StormWeaverHead.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "Signus",
                19.3f,
                () => CalamityWorld1Point2.downedSentinel3,
                ModContent.NPCType<CosmicWraith>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<RuneofCos>(),
                    ["spawnInfo"] = GetLocalization("NPCs.CosmicWraith.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "DevourerofGods",
                20f,
                () => CalamityWorld1Point2.downedDoG,
                ModContent.NPCType<DevourerofGodsHead>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<CosmicWorm>(),
                    ["spawnInfo"] = GetLocalization("NPCs.DevourerofGodsHead.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "Bumblebirb",
                20.5f,
                () => CalamityWorld1Point2.downedBumble,
                ModContent.NPCType<Bumblefuck>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<BirbPheromones>(),
                    ["spawnInfo"] = GetLocalization("NPCs.Bumblefuck.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "Yharon",
                21f,
                () => CalamityWorld1Point2.downedYharon,
                ModContent.NPCType<Yharon>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<ChickenEgg>(),
                    ["spawnInfo"] = GetLocalization("NPCs.Yharon.SpawnInfo")
                });
                bossChecklist.Call(
                "LogBoss",
                instance,
                "SupremeCalamitas",
                22f,
                () => CalamityWorld1Point2.downedSCal,
                ModContent.NPCType<SupremeCalamitas>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<EyeofExtinction>(),
                    ["spawnInfo"] = GetLocalization("NPCs.SupremeCalamitas.SpawnInfo")
                });
            }
        }
    	    	
    	public static Color GetNPCColor(NPC npc, Vector2? position = null, bool effects = true, float shadowOverride = 0f)
        {
            return npc.GetAlpha(BuffEffects(npc, GetLightColor(position != null ? (Vector2)position : npc.Center), (shadowOverride != 0f ? shadowOverride : 0f), effects, npc.poisoned, npc.onFire, npc.onFire2, Main.player[Main.myPlayer].detectCreature, false, false, false, npc.venom, npc.midas, npc.ichor, npc.onFrostBurn, false, false, npc.dripping, npc.drippingSlime, npc.loveStruck, npc.stinky));
        }
    	
    	public static Color GetLightColor(Vector2 position)
        {
            return Lighting.GetColor((int)(position.X / 16f), (int)(position.Y / 16f));
        }
    	
    	public static Color BuffEffects(Entity codable, Color lightColor, float shadow = 0f, bool effects = true, bool poisoned = false, bool onFire = false, bool onFire2 = false, bool hunter = false, bool noItems = false, bool blind = false, bool bleed = false, bool venom = false, bool midas = false, bool ichor = false, bool onFrostBurn = false, bool burned = false, bool honey = false, bool dripping = false, bool drippingSlime = false, bool loveStruck = false, bool stinky = false)
        {
            float cr = 1f; float cg = 1f; float cb = 1f; float ca = 1f;
			if (effects && honey && Main.rand.NextBool(30))
			{
				int dustID = Dust.NewDust(codable.position, codable.width, codable.height, DustID.Honey, 0f, 0f, 150, default(Color), 1f);
				Main.dust[dustID].velocity.Y = 0.3f;
				Main.dust[dustID].velocity.X *= 0.1f;
				Main.dust[dustID].scale += (float)Main.rand.Next(3, 4) * 0.1f;
				Main.dust[dustID].alpha = 100;
				Main.dust[dustID].noGravity = true;
				Main.dust[dustID].velocity += codable.velocity * 0.1f;
			}
            if (poisoned)
            {
				if (effects && Main.rand.NextBool(30))
				{
					int dustID = Dust.NewDust(codable.position, codable.width, codable.height, DustID.Poisoned, 0f, 0f, 120, default(Color), 0.2f);
					Main.dust[dustID].noGravity = true;
					Main.dust[dustID].fadeIn = 1.9f;
				}
                cr *= 0.65f;
                cb *= 0.75f;
            }
			if (venom)
			{
				if (effects && Main.rand.NextBool(10))
				{
					int dustID = Dust.NewDust(codable.position, codable.width, codable.height, DustID.Venom, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[dustID].noGravity = true;
					Main.dust[dustID].fadeIn = 1.5f;
				}
				cg *= 0.45f;
				cr *= 0.75f;
			}
			if (midas)
			{
				cb *= 0.3f;
				cr *= 0.85f;
			}
			if (ichor)
			{
				if (codable is NPC) 
				{ 
					lightColor = new Color(255, 255, 0, 255); 
				} 
				else
				{ 
					cb = 0f; 
				}
			}
			if (burned)
			{
				if (effects)
				{
					int dustID = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, DustID.Torch, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 2f);
					Main.dust[dustID].noGravity = true;
					Main.dust[dustID].velocity *= 1.8f;
					Main.dust[dustID].velocity.Y -= 0.75f;
				}
				if (codable is Player)
				{
					cr = 1f;
					cb *= 0.6f;
					cg *= 0.7f;
				}
			}
			if (onFrostBurn)
			{
				if (effects)
				{
					if (Main.rand.Next(4) < 3)
					{
						int dustID = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, DustID.IceTorch, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
						Main.dust[dustID].noGravity = true;
						Main.dust[dustID].velocity *= 1.8f;
						Main.dust[dustID].velocity.Y -= 0.5f;
						if (Main.rand.NextBool(4))
						{
							Main.dust[dustID].noGravity = false;
							Main.dust[dustID].scale *= 0.5f;
						}
					}
					Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 0.1f, 0.6f, 1f);
				}
				if (codable is Player)
				{
					cr *= 0.5f;
					cg *= 0.7f;
				}
			}
            if (onFire)
            {
				if (effects)
				{
					if (!Main.rand.NextBool(4))
					{
						int dustID = Dust.NewDust(codable.position - new Vector2(2f, 2f), codable.width + 4, codable.height + 4, DustID.Torch, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
						Main.dust[dustID].noGravity = true;
						Main.dust[dustID].velocity *= 1.8f;
						Main.dust[dustID].velocity.Y -= 0.5f;
						if (Main.rand.NextBool(4))
						{
							Main.dust[dustID].noGravity = false;
							Main.dust[dustID].scale *= 0.5f;
						}
					}
					Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
				}
				if (codable is Player)
				{
					cb *= 0.6f;
					cg *= 0.7f;
				}
            }
			if (dripping && shadow == 0f && !Main.rand.NextBool(4))
			{
				Vector2 position = codable.position;
				position.X -= 2f; position.Y -= 2f;
				if (Main.rand.NextBool(2))
				{
					int dustID = Dust.NewDust(position, codable.width + 4, codable.height + 2, DustID.Wet, 0f, 0f, 50, default(Color), 0.8f);
					if (Main.rand.NextBool(2)) 
					{
						Main.dust[dustID].alpha += 25;
					}
					if (Main.rand.NextBool(2)) 
					{
						Main.dust[dustID].alpha += 25;
					}
					Main.dust[dustID].noLight = true;
					Main.dust[dustID].velocity *= 0.2f;
					Main.dust[dustID].velocity.Y += 0.2f;
					Main.dust[dustID].velocity += codable.velocity;
				}
				else
				{
					int dustID = Dust.NewDust(position, codable.width + 8, codable.height + 8, DustID.Wet, 0f, 0f, 50, default(Color), 1.1f);
					if (Main.rand.NextBool(2)) 
					{
						Main.dust[dustID].alpha += 25;
					}
					if (Main.rand.NextBool(2)) 
					{
						Main.dust[dustID].alpha += 25;
					}
					Main.dust[dustID].noLight = true;
					Main.dust[dustID].noGravity = true;
					Main.dust[dustID].velocity *= 0.2f;
					Main.dust[dustID].velocity.Y += 1f;
					Main.dust[dustID].velocity += codable.velocity;
				}
			}
			if (drippingSlime && shadow == 0f)
			{
				int alpha = 175;
				Color newColor = new Color(0, 80, 255, 100);
				if (!Main.rand.NextBool(4))
				{
					if (Main.rand.NextBool(2))
					{
						Vector2 position2 = codable.position;
						position2.X -= 2f; position2.Y -= 2f;
						int dustID = Dust.NewDust(position2, codable.width + 4, codable.height + 2, DustID.TintableDust, 0f, 0f, alpha, newColor, 1.4f);
						if (Main.rand.NextBool(2)) 
						{
							Main.dust[dustID].alpha += 25;
						}
						if (Main.rand.NextBool(2)) 
						{
							Main.dust[dustID].alpha += 25;
						}
						Main.dust[dustID].noLight = true;
						Main.dust[dustID].velocity *= 0.2f;
						Main.dust[dustID].velocity.Y += 0.2f;
						Main.dust[dustID].velocity += codable.velocity;
					}
				}
				cr *= 0.8f;
				cg *= 0.8f;
			}
            if (onFire2)
            {
				if (effects)
				{
					if (!Main.rand.NextBool(4))
					{
						int dustID = Dust.NewDust(codable.position - new Vector2(2f, 2f), codable.width + 4, codable.height + 4, DustID.CursedTorch, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
						Main.dust[dustID].noGravity = true;
						Main.dust[dustID].velocity *= 1.8f;
						Main.dust[dustID].velocity.Y -= 0.5f;
						if (Main.rand.NextBool(4))
						{
							Main.dust[dustID].noGravity = false;
							Main.dust[dustID].scale *= 0.5f;
						}
					}
					Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
				}
				if (codable is Player)
				{
					cb *= 0.6f;
					cg *= 0.7f;
				}
            }
            if (noItems)
            {
                cr *= 0.65f;
                cg *= 0.8f;
            }
            if (blind)
            {
                cr *= 0.7f;
                cg *= 0.65f;
            }
            if (bleed)
            {
				bool dead = (codable is Player ? ((Player)codable).dead : codable is NPC ? ((NPC)codable).life <= 0 : false);
				if (effects && !dead && Main.rand.NextBool(30))
				{
					int dustID = Dust.NewDust(codable.position, codable.width, codable.height, DustID.Blood, 0f, 0f, 0, default(Color), 1f);
					Main.dust[dustID].velocity.Y += 0.5f;
					Main.dust[dustID].velocity *= 0.25f;
				}
                cg *= 0.9f;
                cb *= 0.9f;
            }
			if (loveStruck && effects && shadow == 0f && Main.instance.IsActive && !Main.gamePaused && Main.rand.NextBool(5))
			{
				Vector2 value = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
				value.Normalize();
				value.X *= 0.66f;
				int goreID = Gore.NewGore(codable.GetSource_FromThis(), codable.position + new Vector2((float)Main.rand.Next(codable.width + 1), (float)Main.rand.Next(codable.height + 1)), value * (float)Main.rand.Next(3, 6) * 0.33f, 331, (float)Main.rand.Next(40, 121) * 0.01f);
				Main.gore[goreID].sticky = false;
				Main.gore[goreID].velocity *= 0.4f;
				Main.gore[goreID].velocity.Y -= 0.6f;
			}
			if (stinky && shadow == 0f)
			{
				cr *= 0.7f;
				cb *= 0.55f;
				if (effects && Main.rand.NextBool(5)&& Main.instance.IsActive && !Main.gamePaused)
				{
					Vector2 value2 = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
					value2.Normalize(); value2.X *= 0.66f; value2.Y = Math.Abs(value2.Y);
					Vector2 vector = value2 * (float)Main.rand.Next(3, 5) * 0.25f;
					int dustID = Dust.NewDust(codable.position, codable.width, codable.height, DustID.FartInAJar, vector.X, vector.Y * 0.5f, 100, default(Color), 1.5f);
					Main.dust[dustID].velocity *= 0.1f;
					Main.dust[dustID].velocity.Y -= 0.5f;
				}
			}
			lightColor.R = (byte)((float)lightColor.R * cr);
			lightColor.G = (byte)((float)lightColor.G * cg);
			lightColor.B = (byte)((float)lightColor.B * cb);
			lightColor.A = (byte)((float)lightColor.A * ca);			
			if (codable is NPC) 
			{
				NPCLoader.DrawEffects((NPC)codable, ref lightColor);
			}
            if (hunter && (codable is NPC ? ((NPC)codable).lifeMax > 1 : true))
            {
				if (effects && !Main.gamePaused && Main.instance.IsActive && Main.rand.NextBool(50))
				{
					int dustID = Dust.NewDust(codable.position, codable.width, codable.height, DustID.MagicMirror, 0f, 0f, 150, default(Color), 0.8f);
					Main.dust[dustID].velocity *= 0.1f;
					Main.dust[dustID].noLight = true;
				}
				byte colorR = 50, colorG = 255, colorB = 50;
				if(codable is NPC && !(((NPC)codable).friendly || ((NPC)codable).catchItem > 0 || (((NPC)codable).damage == 0 && ((NPC)codable).lifeMax == 5)))
				{
					colorR = 255; colorG = 50;
				}
                if (!(codable is NPC) && lightColor.R < 150) 
                { 
                	lightColor.A = Main.mouseTextColor; 
                }
                if (lightColor.R < colorR) 
                { 
                	lightColor.R = colorR; 
                }
                if (lightColor.G < colorG) 
                { 
                	lightColor.G = colorG;
                }
                if (lightColor.B < colorB) 
                { 
                	lightColor.B = colorB; 
                }
            }
            return lightColor;
        }
    	
    	public static void DrawTexture(object sb, Texture2D texture, int shader, Entity codable, Color? overrideColor = null, bool drawCentered = false)
        {
            Color lightColor = (overrideColor != null ? (Color)overrideColor : codable is NPC ? GetNPCColor(((NPC)codable), codable.Center, false) : codable is Projectile ? ((Projectile)codable).GetAlpha(GetLightColor(codable.Center)) : GetLightColor(codable.Center));
            int frameCount = (codable is NPC ? Main.npcFrameCount[((NPC)codable).type] : 1);
            Rectangle frame = (codable is NPC ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Width, texture.Height));
            float scale = (codable is NPC ? ((NPC)codable).scale : ((Projectile)codable).scale);
            float rotation = (codable is NPC ? ((NPC)codable).rotation : ((Projectile)codable).rotation);
            int spriteDirection = (codable is NPC ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection);
			float offsetY = (codable is NPC ? ((NPC)codable).gfxOffY : 0f);
            DrawTexture(sb, texture, shader, codable.position + new Vector2(0f, offsetY), codable.width, codable.height, scale, rotation, spriteDirection, frameCount, frame, lightColor, drawCentered);
        }
    	
    	public static void DrawTexture(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float scale, float rotation, int direction, int framecount, Rectangle frame, Color? overrideColor = null, bool drawCentered = false)
        {
            Vector2 origin = new Vector2((float)(texture.Width / 2), (float)(texture.Height / framecount / 2));
            Color lightColor = overrideColor != null ? (Color)overrideColor : GetLightColor(position + new Vector2(width * 0.5f, height * 0.5f));
			if (sb is List<DrawData>)
			{
				DrawData dd = new DrawData(texture, GetDrawPosition(position, origin, width, height, texture.Width, texture.Height, framecount, scale, drawCentered), frame, lightColor, rotation, origin, scale, direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
				dd.shader = shader;
				((List<DrawData>)sb).Add(dd);
			}
			else if (sb is SpriteBatch)
			{
				bool applyDye = shader > 0;
				if (applyDye)
				{
					((SpriteBatch)sb).End();
					((SpriteBatch)sb).Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
					GameShaders.Armor.ApplySecondary(shader, Main.player[Main.myPlayer], null);				
				}
				((SpriteBatch)sb).Draw(texture, GetDrawPosition(position, origin, width, height, texture.Width, texture.Height, framecount, scale, drawCentered), frame, lightColor, rotation, origin, scale, direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);				
				if (applyDye)
				{
					((SpriteBatch)sb).End();
					((SpriteBatch)sb).Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
				}
			}
        }
    	
    	public static Vector2 GetDrawPosition(Vector2 position, Vector2 origin, int width, int height, int texWidth, int texHeight, int framecount, float scale, bool drawCentered = false)
        {
			Vector2 screenPos = new Vector2((int)Main.screenPosition.X, (int)Main.screenPosition.Y);
			if(drawCentered)
			{
				Vector2 texHalf = new Vector2(texWidth / 2, texHeight / framecount / 2);
				return (position + new Vector2(width * 0.5f, height * 0.5f)) - (texHalf * scale) + (origin * scale) - screenPos;	
			}
			return position - screenPos + new Vector2(width * 0.5f, height) - new Vector2(texWidth * scale / 2f, texHeight * scale / (float)framecount) + (origin * scale) + new Vector2(0f, 5f);
        }
    	
    	public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			CalamityModClassic1Point2MessageType msgType = (CalamityModClassic1Point2MessageType)reader.ReadByte();
			switch (msgType)
			{
				case CalamityModClassic1Point2MessageType.Providence:
					Providence providence = Main.npc[reader.ReadInt32()].ModNPC as Providence;
					if (providence != null && providence.NPC.active)
					{
						providence.HandlePacket(reader);
					}
					break;
				case CalamityModClassic1Point2MessageType.DoG:
					DevourerofGodsHead devourer = Main.npc[reader.ReadInt32()].ModNPC as DevourerofGodsHead;
					if (devourer != null && devourer.NPC.active)
					{
						devourer.HandlePacket(reader);
					}
					break;
				case CalamityModClassic1Point2MessageType.Yharon:
					Yharon yharon = Main.npc[reader.ReadInt32()].ModNPC as Yharon;
					if (yharon != null && yharon.NPC.active)
					{
						yharon.HandlePacket(reader);
					}
					break;
				case CalamityModClassic1Point2MessageType.SupremeCalamitas:
					SupremeCalamitas calamitas = Main.npc[reader.ReadInt32()].ModNPC as SupremeCalamitas;
					if (calamitas != null && calamitas.NPC.active)
					{
						calamitas.HandlePacket(reader);
					}
					break;
				case CalamityModClassic1Point2MessageType.Polterghast:
					Polterghast polterghast = Main.npc[reader.ReadInt32()].ModNPC as Polterghast;
					if (polterghast != null && polterghast.NPC.active)
					{
						polterghast.HandlePacket(reader);
					}
					break;
				default:
					break;
			}
		}
    	
    	public override object Call(params object[] args)
		{
			return ModSupport1Point2.Call(args);
		}
    }
    
    enum CalamityModClassic1Point2MessageType : byte
	{
		Providence,
		DoG,
		Yharon,
		SupremeCalamitas,
		Polterghast
	}
}