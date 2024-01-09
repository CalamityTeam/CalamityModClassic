using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class ApocalypticExpulsor : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture = "CalamityModClassic1Point0/Items/Weapons/ApocalypticExpulsor");
		return true;
	}
	
    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Apocalyptic Expulsor");
        Item.damage = 40;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 46;
        Item.height = 20;
        //AddTooltip("Ping!");
        //AddTooltip2("50% chance to not consume ammo and can only fire Hyperius Bullets");
        Item.useTime = 35;
        Item.useAnimation = 35;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4.5f;
        Item.value = 130000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item38;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 16f;
        Item.shoot = Mod.Find<ModProjectile>("HyperiusBullet").Type;
        Item.useAmmo = Mod.Find<ModProjectile>("HyperiusBullet").Type;
    }
    
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
    	if (Main.rand.Next(0, 101) <= 50)
    		return false;
    	return true;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
            float speedX = velocity.X;
            float speedY = velocity.Y;
        int num126 = Main.rand.Next(4, 6);
        for (int num127 = 0; num127 < num126; num127++)
        {
            float num128 = speedX;
            float num129 = speedY;
            num128 += (float)Main.rand.Next(-40, 41) * 0.05f;
            num129 += (float)Main.rand.Next(-40, 41) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, speedX, speedY, type, damage, knockback, player.whoAmI, 0f, player.direction);
        }
        float SpeedA = speedX;
   		float SpeedB = speedY;
        int num6 = Main.rand.Next(5, 6);
        for (int index = 0; index < num6; ++index)
        {
      	 	float num7 = speedX;
            float num8 = speedY;
            float SpeedX = speedX + (float) Main.rand.Next(-40, 41) * 0.05f;
            float SpeedY = speedY + (float) Main.rand.Next(-40, 41) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
        }
        return false;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "BarofLife", 5);
        recipe.AddIngredient(ItemID.TacticalShotgun, 1);
        recipe.AddIngredient(null, "CoreofCalamity");
        recipe.AddIngredient(ItemID.Xenopopper, 1);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}