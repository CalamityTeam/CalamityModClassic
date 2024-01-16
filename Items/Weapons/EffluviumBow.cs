using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class EffluviumBow : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/5.Cryogen/EffluviumBow");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Effluvium Bow");
        Item.damage = 45;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 18;
        Item.height = 62;
        Item.useTime = 28;
        Item.useAnimation = 28;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        ////Tooltip.SetDefault("Fires mist arrows that can freeze enemies");
        Item.knockBack = 3.75f;
        Item.value = 425000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("MistArrow").Type;
		Item.shootSpeed = 12f;
		Item.useAmmo = 40;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CryoBar", 7);
        recipe.AddTile(TileID.IceMachine);
        recipe.Register();
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	float SpeedA = velocity.X;
   		float SpeedB = velocity.Y;
        int num6 = Main.rand.Next(1, 3);
        for (int index = 0; index < num6; ++index)
        {
      	 	float num7 = velocity.X;
            float num8 = velocity.Y;
            float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
            float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
	        Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),Mod.Find<ModProjectile>("MistArrow").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
    	}
    	return false;
	}
}}