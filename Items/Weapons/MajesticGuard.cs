using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class MajesticGuard : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/MajesticGuard");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Majestic Guard");
		Item.width = 124;  //The width of the .png file in pixels divided by 2.
		Item.damage = 60;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 22;
		Item.useStyle = 1;
		Item.useTime = 22;
		Item.useTurn = true;
		////Tooltip.SetDefault("Has a chance to lower enemy defense by 10 when striking them\nIf enemy defense is 0 or below your attacks will heal you");
		Item.knockBack = 7.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 124;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 600000;  //Value is calculated in copper coins.
		Item.rare = 5;  //Ranges from 1 to 11.
	}
	
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		if (Main.rand.Next(5) == 0)
		{
			target.defense -= 10;
		}
		if (target.defense <= 0)
		{
	    	player.statLife += 6;
	    	player.HealEffect(6);
		}
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.AdamantiteSword);
		recipe.AddIngredient(ItemID.SoulofMight, 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.TitaniumSword);
		recipe.AddIngredient(ItemID.SoulofMight, 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
