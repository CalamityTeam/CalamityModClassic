using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class StormRuler : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Storm Ruler");
		//Tooltip.SetDefault("Only a storm can fell a greatwood");
	}

	public override void SetDefaults()
	{
		Item.width = 64;  //The width of the .png file in pixels divided by 2.
		Item.damage = 135;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 25;
		Item.useTime = 25;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 6.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 64;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 3000000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Cyan;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("StormRuler").Type;
		Item.shootSpeed = 20f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CoreofCinder", 3);
		recipe.AddIngredient(null, "WindBlade");
		recipe.AddIngredient(null, "StormSaber");
		recipe.AddIngredient(ItemID.FragmentSolar, 10);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(5))
		{
			int num250 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, DustID.Flare_Blue, (float)(player.direction * 2), 0f, 150, default(Color), 1.3f);
			Main.dust[num250].velocity *= 0.2f;
		}
    }
}}
