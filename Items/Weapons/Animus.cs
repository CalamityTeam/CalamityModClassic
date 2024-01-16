using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Animus : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/Animus");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Animus");
		Item.width = 80;  //The width of the .png file in pixels divided by 2.
		Item.damage = 500;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 12;
		Item.useStyle = 1;
		Item.useTime = 12;
		Item.useTurn = true;
		////Tooltip.SetDefault("Great for impersonating devs!\nCan randomly inflict massive damage to enemies");
		Item.knockBack = 20f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 80;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 8000000;  //Value is calculated in copper coins.
		Item.expert = true;  //Ranges from 1 to 11.
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "BladeofEnmity");
		recipe.AddIngredient(null, "ShadowspecBar", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 5000);
		if (Main.rand.Next(10) == 0)
		{
			Item.damage = 600;
		}
		else if (Main.rand.Next(50) == 0)
		{
			Item.damage = 1000;
		}
		else if (Main.rand.Next(100) == 0)
		{
			Item.damage = 2500;
		}
		else if (Main.rand.Next(500) == 0)
		{
			Item.damage = 50000;
		}
		else
		{
			Item.damage = 250;
		}
	}
}}
