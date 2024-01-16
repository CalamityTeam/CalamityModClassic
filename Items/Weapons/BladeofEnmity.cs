using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class BladeofEnmity : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/BladeofEnmity");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Blade of Enmity");
		Item.width = 60;  //The width of the .png file in pixels divided by 2.
		Item.damage = 175;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 8;
		Item.useStyle = 1;
		Item.useTime = 8;
		Item.useTurn = true;
		Item.knockBack = 8f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 60;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 1250000;  //Value is calculated in copper coins.
		Item.rare = 9;  //Ranges from 1 to 11.
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "BarofLife", 5);
		recipe.AddIngredient(null, "CoreofCalamity", 3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.BrokenArmor, 1800);
		target.AddBuff(BuffID.Weak, 300);
		target.AddBuff(BuffID.Slow, 1200);
		target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 2000);
	}
}}
