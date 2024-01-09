using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TemporalFloeSword : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Temporal Floe Sword");
			//Tooltip.SetDefault("The iceman cometh...");
		}

	public override void SetDefaults()
	{
		Item.width = 42;  //The width of the .png file in pixels divided by 2.
		Item.damage = 85;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 16;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 16;
		Item.useTurn = true;
		Item.knockBack = 6;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 50;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 1500000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Yellow;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("TemporalFloeSwordProjectile").Type;
		Item.shootSpeed = 16f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CryoBar", 15);
		recipe.AddIngredient(ItemID.Ectoplasm, 5);
		recipe.AddTile(TileID.IceMachine);	
		recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(3))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.BreatheBubble);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	if (Main.rand.NextBool(3))
    	{
    		target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 120);
    	}
		target.AddBuff(BuffID.Chilled, 900);
		target.AddBuff(BuffID.Frostburn, 600);
	}
}}
