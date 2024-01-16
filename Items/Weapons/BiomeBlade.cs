using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class BiomeBlade : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/BiomeBlade");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Biome Blade");
		Item.width = 36;  //The width of the .png file in pixels divided by 2.
		Item.damage = 40;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 24;
		Item.useTime = 24;  //Ranges from 1 to 55.
		////Tooltip.SetDefault("Fires different projectiles based on what biome you're in");
		Item.useTurn = true;
		Item.useStyle = 1;
		Item.knockBack = 6;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 36;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 300000;  //Value is calculated in copper coins.
		Item.rare = 4;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("BiomeOrb").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.WoodenSword);
		recipe.AddIngredient(ItemID.DirtBlock, 10);
		recipe.AddIngredient(ItemID.SandBlock, 10);
		recipe.AddIngredient(ItemID.IceBlock, 10);
		recipe.AddIngredient(ItemID.EbonstoneBlock, 10);
		recipe.AddIngredient(ItemID.GlowingMushroom, 10);
		recipe.AddIngredient(ItemID.Marble, 10);
		recipe.AddIngredient(ItemID.Granite, 10);
		recipe.AddIngredient(ItemID.Hellstone, 10);
		recipe.AddIngredient(ItemID.Coral, 5);
		recipe.AddIngredient(ItemID.PearlstoneBlock, 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WoodenSword);
		recipe.AddIngredient(ItemID.DirtBlock, 10);
		recipe.AddIngredient(ItemID.SandBlock, 10);
		recipe.AddIngredient(ItemID.IceBlock, 10);
		recipe.AddIngredient(ItemID.CrimstoneBlock, 10);
		recipe.AddIngredient(ItemID.GlowingMushroom, 10);
		recipe.AddIngredient(ItemID.Marble, 10);
		recipe.AddIngredient(ItemID.Granite, 10);
		recipe.AddIngredient(ItemID.Hellstone, 10);
		recipe.AddIngredient(ItemID.Coral, 5);
		recipe.AddIngredient(ItemID.PearlstoneBlock, 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 0);
        }
    }
}}
