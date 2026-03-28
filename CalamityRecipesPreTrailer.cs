using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer
{
	internal class CalamityRecipesPreTrailer
	{
		public static void AddRecipes()
		{
			//EditTerraBladeRecipe();
			AddPotionRecipes();
			AddToolRecipes();
			AddProgressionRecipes();
			AddEarlyGameWeaponRecipes();
			AddEarlyGameAccessoryRecipes();
			AddAnkhShieldRecipes();
			AddAlternateHardmodeRecipes();

			// Leather from Vertebrae, for Crimson worlds
			Recipe.Create(ItemID.Leather)
				.AddIngredient(ItemID.Vertebrae, 5)
				.AddTile(TileID.WorkBenches)
				.Register();

			// Black Lens
			Recipe.Create(ItemID.BlackLens)
				.AddIngredient(ItemID.Lens)
				.AddIngredient(ItemID.BlackDye)
				.AddTile(TileID.DyeVat)
				.Register();

			// Ectoplasm from Ectoblood
			Recipe.Create(ItemID.Ectoplasm)
			.AddIngredient(null, "Ectoblood", 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Rocket I from Empty Bullet
			Recipe.Create(ItemID.RocketI, 20)
			.AddIngredient(ItemID.EmptyBullet, 20)
			.AddIngredient(ItemID.ExplosivePowder, 1)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Life Crystal
			Recipe.Create(ItemID.LifeCrystal)
			.AddIngredient(ItemID.Bone, 5)
			.AddIngredient(ItemID.PinkGel)
			.AddIngredient(ItemID.HealingPotion)
			.AddIngredient(ItemID.Ruby)
			.AddTile(TileID.Anvils)
			.Register();

			// Life Fruit
			Recipe.Create(ItemID.LifeFruit)
			.AddIngredient(null, "PlantyMush", 10)
			.AddIngredient(null, "LivingShard")
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}

		/*
		// Change Terra Blade's recipe to require 7 Living Shards (forces the Blade to be post-Plantera)
		private static void EditTerraBladeRecipe()
		{
			List<Recipe> rec = Main.recipe.ToList();
			rec.Where(x => x.createItem.type == ItemID.TerraBlade).ToList().ForEach(s =>
			{
				for (int i = 0; i < s.requiredItem.Count; i++)
				{
					s.requiredItem[i] = new Item();
				}
				s.requiredItem[0].SetDefaults(ItemID.TrueNightsEdge, false);
				s.requiredItem[0].stack = 1;
				s.requiredItem[1].SetDefaults(ItemID.TrueExcalibur, false);
				s.requiredItem[1].stack = 1;
				s.requiredItem[2].SetDefaults(CalamityModClassicPreTrailer.Instance.Find<ModItem>("LivingShard").Type, false);
				s.requiredItem[2].stack = 7;

				s.createItem.SetDefaults(ItemID.TerraBlade, false);
				s.createItem.stack = 1;
			});
		}
		*/

		#region Potions
		// Equivalent Blood Orb recipes for almost all vanilla potions
		private static void AddPotionRecipes()
		{
			short[] potions = new[]
			{
				ItemID.WormholePotion,
				ItemID.TeleportationPotion,
				ItemID.SwiftnessPotion,
				ItemID.FeatherfallPotion,
				ItemID.GravitationPotion,
				ItemID.ShinePotion,
				ItemID.InvisibilityPotion,
				ItemID.NightOwlPotion,
				ItemID.SpelunkerPotion,
				ItemID.HunterPotion,
				ItemID.TrapsightPotion,
				ItemID.BattlePotion,
				ItemID.CalmingPotion,
				ItemID.WrathPotion,
				ItemID.RagePotion,
				ItemID.ThornsPotion,
				ItemID.IronskinPotion,
				ItemID.EndurancePotion,
				ItemID.RegenerationPotion,
				ItemID.LifeforcePotion,
				ItemID.HeartreachPotion,
				ItemID.TitanPotion,
				ItemID.ArcheryPotion,
				ItemID.AmmoReservationPotion,
				ItemID.MagicPowerPotion,
				ItemID.ManaRegenerationPotion,
				ItemID.SummoningPotion,
				ItemID.InfernoPotion,
				ItemID.WarmthPotion,
				ItemID.ObsidianSkinPotion,
				ItemID.GillsPotion,
				ItemID.WaterWalkingPotion,
				ItemID.FlipperPotion,
				ItemID.BuilderPotion,
				ItemID.MiningPotion,
				ItemID.FishingPotion,
				ItemID.CratePotion,
				ItemID.SonarPotion,
				ItemID.GenderChangePotion,
				ItemID.LovePotion,
				ItemID.StinkPotion
			};
			Recipe r;

			foreach (var potion in potions)
			{
				Recipe.Create(potion)
				.AddIngredient(null, "BloodOrb", 10)
				.AddIngredient(ItemID.BottledWater)
				.AddTile(TileID.AlchemyTable)
				.Register();
			}
		}
		#endregion

		#region Tools
		// Essential tools such as the Magic Mirror and Rod of Discord
		private static void AddToolRecipes()
		{
			// Magic Mirror
			Recipe.Create(ItemID.MagicMirror)
			.AddRecipeGroup(RecipeGroupID.IronBar, 10)
			.AddIngredient(ItemID.Glass, 10)
			.AddIngredient(ItemID.FallenStar, 10)
			.AddTile(TileID.Anvils)
			.Register();

			// Ice Mirror
			Recipe.Create(ItemID.IceMirror)
			.AddIngredient(ItemID.IceBlock, 20)
			.AddIngredient(ItemID.Glass, 10)
			.AddIngredient(ItemID.FallenStar, 10)
			.AddRecipeGroup(RecipeGroupID.IronBar, 5)
			.AddTile(TileID.Anvils)
			.Register();

			// Shadow Key
			Recipe.Create(ItemID.ShadowKey)
			.AddIngredient(ItemID.GoldenKey)
			.AddIngredient(ItemID.Obsidian, 20)
			.AddIngredient(ItemID.Bone, 5)
			.AddTile(TileID.Anvils)
			.Register();

			// Rod of Discord
			Recipe.Create(ItemID.RodofDiscord)
			.AddIngredient(ItemID.SoulofLight, 30)
			.AddIngredient(ItemID.ChaosFish, 5)
			.AddIngredient(ItemID.PixieDust, 50)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Sky Mill
			Recipe.Create(ItemID.SkyMill)
			.AddIngredient(ItemID.SunplateBlock, 10)
			.AddIngredient(ItemID.Cloud, 5)
			.AddIngredient(ItemID.RainCloud, 3)
			.AddTile(TileID.Anvils)
			.Register();

			// Ice Machine
			Recipe.Create(ItemID.IceMachine)
			.AddIngredient(ItemID.IceBlock, 25)
			.AddIngredient(ItemID.SnowBlock, 15)
			.AddRecipeGroup(RecipeGroupID.IronBar, 3)
			.AddTile(TileID.Anvils)
			.Register();
		}
		#endregion

		#region ProgressionItems
		// Boss summon and progression items
		private static void AddProgressionRecipes()
		{
			// Guide Voodoo Doll
			Recipe.Create(ItemID.GuideVoodooDoll)
			.AddIngredient(ItemID.Leather, 2)
			.AddIngredient(null, "FetidEssence", 2)
			.AddTile(TileID.Hellforge)
			.Register();

			Recipe.Create(ItemID.GuideVoodooDoll)
			.AddIngredient(ItemID.Leather, 2)
			.AddIngredient(null, "BloodlettingEssence", 2)
			.AddTile(TileID.Hellforge)
			.Register();

			// Temple Key
			Recipe.Create(ItemID.TempleKey)
			.AddIngredient(ItemID.JungleSpores, 15)
			.AddIngredient(ItemID.RichMahogany, 15)
			.AddIngredient(ItemID.SoulofNight, 15)
			.AddIngredient(ItemID.SoulofLight, 15)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Lihzahrd Power Cell (NOT Calamity's Old Power Cell)
			Recipe.Create(ItemID.LihzahrdPowerCell)
			.AddIngredient(ItemID.LihzahrdBrick, 15)
			.AddIngredient(null, "CoreofCinder")
			.AddTile(TileID.LihzahrdFurnace)
			.Register();

			// Truffle Worm
			Recipe.Create(ItemID.TruffleWorm)
			.AddIngredient(ItemID.GlowingMushroom, 15)
			.AddIngredient(ItemID.Worm)
			.AddTile(TileID.Autohammer)
			.Register();
		}
		#endregion

		#region EarlyGameWeapons
		// Early game weapons such as Enchanted Sword
		private static void AddEarlyGameWeaponRecipes()
		{
			// Shuriken
			Recipe.Create(ItemID.Shuriken, 50)
			.AddRecipeGroup(RecipeGroupID.IronBar)
			.AddTile(TileID.Anvils)
			.Register();

			// Throwing Knife
			Recipe.Create(ItemID.ThrowingKnife, 50)
			.AddRecipeGroup(RecipeGroupID.IronBar)
			.AddTile(TileID.Anvils)
			.Register();

			// Wand of Sparking
			Recipe.Create(ItemID.WandofSparking)
			.AddIngredient(ItemID.Wood, 5)
			.AddIngredient(ItemID.Torch, 3)
			.AddIngredient(ItemID.FallenStar)
			.AddTile(TileID.Anvils)
			.Register();

			// Starfury w/ Gold Broadsword
			Recipe.Create(ItemID.Starfury)
			.AddIngredient(ItemID.GoldBroadsword)
			.AddIngredient(ItemID.FallenStar, 10)
			.AddIngredient(null, "VictoryShard", 3)
			.AddTile(TileID.Anvils)
			.Register();

			// Starfury w/ Platinum Broadsword
			Recipe.Create(ItemID.Starfury)
			.AddIngredient(ItemID.PlatinumBroadsword)
			.AddIngredient(ItemID.FallenStar, 10)
			.AddIngredient(null, "VictoryShard", 3)
			.AddTile(TileID.Anvils)
			.Register();

			// Enchanted Sword (requires Hardmode materials)
			Recipe.Create(ItemID.EnchantedSword)
			.AddIngredient(null, "VictoryShard", 10)
			.AddIngredient(ItemID.SoulofLight, 15)
			.AddIngredient(ItemID.UnicornHorn, 3)
			.AddIngredient(ItemID.LightShard)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Muramasa w/ Cobalt Bars
			Recipe.Create(ItemID.Muramasa)
			.AddIngredient(ItemID.CobaltBar, 15)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Muramasa w/ Palladium Bars
			Recipe.Create(ItemID.Muramasa)
			.AddIngredient(ItemID.PalladiumBar, 15)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
		#endregion

		#region EarlyGameAccessories
		// Early game accessories such as Cloud in a Bottle
		private static void AddEarlyGameAccessoryRecipes()
		{
			// Cloud in a Bottle
			Recipe.Create(ItemID.CloudinaBottle)
			.AddIngredient(ItemID.Feather, 2)
			.AddIngredient(ItemID.Bottle)
			.AddIngredient(ItemID.Cloud, 25)
			.AddTile(TileID.Anvils)
			.Register();

			// Hermes Boots
			Recipe.Create(ItemID.HermesBoots)
			.AddIngredient(ItemID.Silk, 10)
			.AddTile(TileID.Loom)
			.Register();

			// Blizzard in a Bottle
			Recipe.Create(ItemID.BlizzardinaBottle)
			.AddIngredient(ItemID.Feather, 4)
			.AddIngredient(ItemID.Bottle)
			.AddIngredient(ItemID.SnowBlock, 50)
			.AddTile(TileID.Anvils)
			.Register();

			// Sandstorm in a Bottle
			Recipe.Create(ItemID.SandstorminaBottle)
			.AddIngredient(null, "DesertFeather", 10)
			.AddIngredient(ItemID.Feather, 6)
			.AddIngredient(ItemID.Bottle)
			.AddIngredient(ItemID.SandBlock, 70)
			.AddTile(TileID.Anvils)
			.Register();

			// Frog Leg
			Recipe.Create(ItemID.FrogLeg)
			.AddIngredient(ItemID.Frog, 10)
			.AddTile(TileID.Anvils)
			.Register();

			// Flying Carpet
			Recipe.Create(ItemID.FlyingCarpet)
			.AddIngredient(ItemID.AncientCloth, 10)
			.AddIngredient(ItemID.SoulofLight, 10)
			.AddIngredient(ItemID.SoulofNight, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Aglet
			Recipe.Create(ItemID.Aglet)
			.AddRecipeGroup(RecipeGroupID.IronBar, 5)
			.AddTile(TileID.Anvils)
			.Register();

			// Anklet of the Wind
			Recipe.Create(ItemID.AnkletoftheWind)
			.AddIngredient(ItemID.JungleSpores, 15)
			.AddIngredient(ItemID.Cloud, 15)
			.AddIngredient(ItemID.PinkGel, 5)
			.AddTile(TileID.Anvils)
			.Register();

			// Water Walking Boots
			Recipe.Create(ItemID.WaterWalkingBoots)
			.AddIngredient(ItemID.Leather, 5)
			.AddIngredient(ItemID.WaterWalkingPotion, 8)
			.AddTile(TileID.Anvils)
			.Register();

			// Ice Skates
			Recipe.Create(ItemID.IceSkates)
			.AddIngredient(ItemID.IceBlock, 20)
			.AddIngredient(ItemID.Leather, 5)
			.AddRecipeGroup(RecipeGroupID.IronBar, 5)
			.AddTile(TileID.IceMachine)
			.Register();

			// Lucky Horseshoe w/ Gold Bars
			Recipe.Create(ItemID.LuckyHorseshoe)
			.AddIngredient(ItemID.SunplateBlock, 10)
			.AddIngredient(ItemID.Cloud, 10)
			.AddIngredient(ItemID.GoldBar, 5)
			.AddTile(TileID.Anvils)
			.Register();

			// Lucky Horseshoe w/ Platinum Bars
			Recipe.Create(ItemID.LuckyHorseshoe)
			.AddIngredient(ItemID.SunplateBlock, 10)
			.AddIngredient(ItemID.Cloud, 10)
			.AddIngredient(ItemID.PlatinumBar, 5)
			.AddTile(TileID.Anvils)
			.Register();

			// Shiny Red Balloon
			Recipe.Create(ItemID.ShinyRedBalloon)
			.AddIngredient(ItemID.WhiteString)
			.AddIngredient(ItemID.Gel, 80)
			.AddIngredient(ItemID.Cloud, 40)
			.AddTile(TileID.Solidifier)
			.Register();

			// Lava Charm
			Recipe.Create(ItemID.LavaCharm)
			.AddIngredient(ItemID.LavaBucket, 5)
			.AddIngredient(ItemID.Obsidian, 25)
			.AddRecipeGroup(RecipeGroupID.IronBar, 5)
			.AddTile(TileID.Anvils)
			.Register();

			// Feral Claws
			Recipe.Create(ItemID.FeralClaws)
			.AddIngredient(ItemID.Leather, 10)
			.AddTile(TileID.Anvils)
			.Register();

			// Radar
			Recipe.Create(ItemID.Radar)
			.AddRecipeGroup(RecipeGroupID.IronBar, 5)
			.AddTile(TileID.Anvils)
			.Register();
		}
		#endregion

		#region AnkhShield
		// Every base component for the Ankh Shield
		private static void AddAnkhShieldRecipes()
		{
			// Cobalt Shield w/ Cobalt
			Recipe.Create(ItemID.CobaltShield)
			.AddIngredient(ItemID.CobaltBar, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Cobalt Shield w/ Palladium
			Recipe.Create(ItemID.CobaltShield)
			.AddIngredient(ItemID.PalladiumBar, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Armor Polish (broken armor)
			Recipe.Create(ItemID.ArmorPolish)
			.AddIngredient(ItemID.Bone, 50)
			.AddIngredient(null, "AncientBoneDust", 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Adhesive Bandage (bleeding)
			Recipe.Create(ItemID.AdhesiveBandage)
			.AddIngredient(ItemID.Silk, 10)
			.AddIngredient(ItemID.Gel, 50)
			.AddIngredient(ItemID.GreaterHealingPotion)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Bezoar (poison)
			Recipe.Create(ItemID.Bezoar)
			.AddIngredient(ItemID.Stinger, 15)
			.AddIngredient(null, "MurkyPaste")
			.AddTile(TileID.Anvils)
			.Register();

			// Nazar (curse)
			Recipe.Create(ItemID.Nazar)
			.AddIngredient(ItemID.SoulofNight, 20)
			.AddIngredient(ItemID.Lens, 5)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Vitamins (weakness)
			Recipe.Create(ItemID.Vitamins)
			.AddIngredient(ItemID.BottledWater)
			.AddIngredient(ItemID.Waterleaf, 5)
			.AddIngredient(ItemID.Blinkroot, 5)
			.AddIngredient(ItemID.Daybloom, 5)
			.AddIngredient(null, "BeetleJuice", 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Blindfold (darkness)
			Recipe.Create(ItemID.Blindfold)
			.AddIngredient(ItemID.Silk, 30)
			.AddIngredient(ItemID.SoulofNight, 5)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Trifold Map (confusion)
			Recipe.Create(ItemID.TrifoldMap)
			.AddIngredient(ItemID.Silk, 20)
			.AddIngredient(ItemID.SoulofLight, 3)
			.AddIngredient(ItemID.SoulofNight, 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Fast Clock (slow)
			Recipe.Create(ItemID.FastClock)
			.AddIngredient(ItemID.Timer1Second)
			.AddIngredient(ItemID.PixieDust, 15)
			.AddIngredient(ItemID.SoulofLight, 5)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Megaphone (silence)
			Recipe.Create(ItemID.Megaphone)
			.AddIngredient(ItemID.Wire, 10)
			.AddIngredient(ItemID.HallowedBar, 5)
			.AddIngredient(ItemID.Ruby, 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
		#endregion

		#region HardmodeEquipment
		// Alternate recipes for vanilla Hardmode equipment
		private static void AddAlternateHardmodeRecipes()
		{
			// Avenger Emblem made with Rogue Emblem
			Recipe.Create(ItemID.AvengerEmblem)
			.AddIngredient(null, "RogueEmblem")
			.AddIngredient(ItemID.SoulofMight, 5)
			.AddIngredient(ItemID.SoulofSight, 5)
			.AddIngredient(ItemID.SoulofFright, 5)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();

			// Celestial Magnet
			Recipe.Create(ItemID.CelestialMagnet)
			.AddIngredient(ItemID.FallenStar, 20)
			.AddIngredient(ItemID.SoulofMight, 10)
			.AddIngredient(ItemID.SoulofLight, 5)
			.AddIngredient(ItemID.SoulofNight, 5)
			.AddIngredient(null, "CryoBar", 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Frozen Turtle Shell
			Recipe.Create(ItemID.FrozenTurtleShell)
			.AddIngredient(ItemID.TurtleShell, 3)
			.AddIngredient(null, "EssenceofEleum", 9)
			.AddTile(TileID.MythrilAnvil)
			.Register();

			// Frost Helmet w/ Frigid Bars
			Recipe.Create(ItemID.FrostHelmet)
			.AddIngredient(null, "CryoBar", 6)
			.AddIngredient(ItemID.FrostCore)
			.AddTile(TileID.IceMachine)
			.Register();

			// Frost Breastplate w/ Frigid Bars
			Recipe.Create(ItemID.FrostBreastplate)
			.AddIngredient(null, "CryoBar", 10)
			.AddIngredient(ItemID.FrostCore)
			.AddTile(TileID.IceMachine)
			.Register();

			// Frost Leggings w/ Frigid Bars
			Recipe.Create(ItemID.FrostLeggings)
			.AddIngredient(null, "CryoBar", 8)
			.AddIngredient(ItemID.FrostCore)
			.AddTile(TileID.IceMachine)
			.Register();

			// Terra Blade w/ True Bloody Edge
			Recipe.Create(ItemID.TerraBlade)
			.AddIngredient(null, "TrueBloodyEdge")
			.AddIngredient(ItemID.TrueExcalibur)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
		#endregion

		public static void AddRecipeGroups()
		{
			RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + (" Silt"), new int[]
			{
				ItemID.SiltBlock,
				ItemID.SlushBlock
			});
			RecipeGroup.RegisterGroup("SiltGroup", group);

			group = new RecipeGroup(() => Lang.misc[37] + (" Lunar Pickaxe"), new int[]
			{
				ItemID.SolarFlarePickaxe,
				ItemID.VortexPickaxe,
				ItemID.NebulaPickaxe,
				ItemID.StardustPickaxe
			});
			RecipeGroup.RegisterGroup("LunarPickaxe", group);

			group = new RecipeGroup(() => Lang.misc[37] + (" Lunar Hamaxe"), new int[]
			{
				ItemID.LunarHamaxeSolar,
				ItemID.LunarHamaxeVortex,
				ItemID.LunarHamaxeNebula,
				ItemID.LunarHamaxeStardust
			});
			RecipeGroup.RegisterGroup("LunarHamaxe", group);

			group = new RecipeGroup(() => Lang.misc[37] + (" Wings"), new int[]
			{
				ItemID.DemonWings,
				ItemID.AngelWings,
				ItemID.RedsWings,
				ItemID.ButterflyWings,
				ItemID.FairyWings,
				ItemID.HarpyWings,
				ItemID.BoneWings,
				ItemID.FlameWings,
				ItemID.FrozenWings,
				ItemID.GhostWings,
				ItemID.SteampunkWings,
				ItemID.LeafWings,
				ItemID.BatWings,
				ItemID.BeeWings,
				ItemID.DTownsWings,
				ItemID.WillsWings,
				ItemID.CrownosWings,
				ItemID.CenxsWings,
				ItemID.TatteredFairyWings,
				ItemID.SpookyWings,
				ItemID.Hoverboard,
				ItemID.FestiveWings,
				ItemID.BeetleWings,
				ItemID.FinWings,
				ItemID.FishronWings,
				ItemID.MothronWings,
				ItemID.WingsSolar,
				ItemID.WingsVortex,
				ItemID.WingsNebula,
				ItemID.WingsStardust,
				ItemID.Yoraiz0rWings,
				ItemID.JimsWings,
				ItemID.SkiphsWings,
				ItemID.LokisWings,
				ItemID.BetsyWings,
				ItemID.ArkhalisWings,
				ItemID.LeinforsWings,
				CalamityModClassicPreTrailer.Instance.Find<ModItem>("SkylineWings").Type,
				CalamityModClassicPreTrailer.Instance.Find<ModItem>("StarlightWings").Type,
				CalamityModClassicPreTrailer.Instance.Find<ModItem>("AureateWings").Type,
				CalamityModClassicPreTrailer.Instance.Find<ModItem>("DiscordianWings").Type,
				CalamityModClassicPreTrailer.Instance.Find<ModItem>("TarragonWings").Type,
				CalamityModClassicPreTrailer.Instance.Find<ModItem>("XerocWings").Type,
				CalamityModClassicPreTrailer.Instance.Find<ModItem>("HadarianWings").Type
			});
			RecipeGroup.RegisterGroup("WingsGroup", group);
		}
	}
}
